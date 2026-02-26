using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using IDAProject.Web.Db.MainDatabase;
using IDAProject.Web.Helpers;
using IDAProject.Web.Api.Models.Auth;

namespace IDAProject.Web.Api.Repositories.IdentityStores
{
    public class AppUserStore : UserStore<AppIdentityUser, AppIdentityRole, IDAProjectContext, int>
    {
        public AppUserStore(IDAProjectContext context) : base(context)
        {
        }


        public override async Task<AppIdentityUser> FindByIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            var id = int.Parse(userId);
            var user = await Context.AspNetUsers.FirstOrDefaultAsync(x => x.Id == id);

            var result = GetAppIdentityUser(user);
            return result!;
        }

        public override async Task<AppIdentityUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            var user = await Context.AspNetUsers.FirstOrDefaultAsync(x => x.NormalizedUserName == normalizedUserName);

            var result = GetAppIdentityUser(user);
            return result!;
        }


        public override async Task<IdentityResult> CreateAsync(AppIdentityUser user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var emp = await Context.Employees.SingleAsync(x => x.Id == user.EmployeeId);
            if (emp == null)
            {
                throw new InvalidOperationException($"Could not find the employee with the specified id: {user.EmployeeId}");
            }

            var keyNormalizer = new UpperInvariantLookupNormalizer();
            var normalizedEmail = keyNormalizer.NormalizeEmail(emp.Email);

            var aspNetUser = new AspNetUser
            {
                UserName = user.UserName,
                NormalizedUserName = user.NormalizedUserName,
                PasswordHash = user.PasswordHash,
                Email = emp.Email,
                EmployeeId = user.EmployeeId,
                NormalizedEmail = normalizedEmail,
                IsActive = true,
                EmailConfirmed = user.EmailConfirmed,
                LockoutEnabled = user.LockoutEnabled,
                AccessFailedCount = 0,
                PhoneNumber = emp.CellPhoneNumber,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                SecurityStamp = user.SecurityStamp,
                ConcurrencyStamp = user.ConcurrencyStamp,
                TwoFactorEnabled = user.LockoutEnabled,
                UserCulture = user.UserCulture,
            };

            await Context.AspNetUsers.AddAsync(aspNetUser);

            await SaveChanges(cancellationToken);

            return IdentityResult.Success;
        }

        public override async Task<IdentityResult> UpdateAsync(AppIdentityUser user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var dbUser = await Context.AspNetUsers.FirstOrDefaultAsync(x => x.Id == user.Id);
            dbUser!.ConcurrencyStamp = Guid.NewGuid().ToString();

            try
            {
                await SaveChanges(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                return IdentityResult.Failed(ErrorDescriber.ConcurrencyFailure());
            }
            return IdentityResult.Success;
        }


        public override async Task AddToRoleAsync(AppIdentityUser user, string normalizedRoleName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (string.IsNullOrWhiteSpace(normalizedRoleName))
            {
                throw new ArgumentException("Role name cannot be empty.", nameof(normalizedRoleName));
            }
            var roleEntity = await FindRoleAsync(normalizedRoleName, cancellationToken);
            if (roleEntity == null)
            {
                throw new InvalidOperationException($"Role: {normalizedRoleName} not found.");
            }

            var dbUser = Context.AspNetUsers.FirstOrDefault(x => x.Id == user.Id);
            var dbRole = Context.AspNetRoles.FirstOrDefault(x => x.Id == roleEntity.Id);
            if (dbRole == null)
            {
                throw new InvalidOperationException($"Role: {normalizedRoleName} not found.");
            }

            var userRole = new AspNetUserRole
            {
                UserId = user.Id,
                RoleId = roleEntity.Id
            };
            Context.AspNetUserRoles.Add(userRole);
            Context.SaveChanges();
        }

        protected override IdentityUserRole<int> CreateUserRole(AppIdentityUser user, AppIdentityRole role)
        {
            return new IdentityUserRole<int>
            {
                UserId = user.Id,
                RoleId = role.Id
            };
        }

        protected override async Task<AppIdentityRole> FindRoleAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return null!;
            }
            AppIdentityRole? result = null;
            var role = await Context.AspNetRoles.FirstOrDefaultAsync(x => x.NormalizedName == normalizedRoleName);

            if (role != null)
            {
                result = new AppIdentityRole
                {
                    Id = role.Id,
                    Name = role.Name,
                    NormalizedName = role.NormalizedName,
                    ConcurrencyStamp = role.ConcurrencyStamp
                };
            }

            return result!;
        }



        protected override async Task<IdentityUserRole<int>> FindUserRoleAsync(int userId, int roleId, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return null!;
            }

            var hasUserRole = await Context.AspNetUserRoles.AnyAsync(x => x.UserId == userId && x.RoleId == roleId);

            IdentityUserRole<int>? result = null;

            if (hasUserRole)
            {
                result = new IdentityUserRole<int>
                {
                    UserId = userId,
                    RoleId = roleId
                };
            }

            return result!;
        }


        private AppIdentityUser? GetAppIdentityUser(AspNetUser? user)
        {
            AppIdentityUser? result = null;
            if (user != null)
            {
                var employee = Context.Employees.Single(x => x.Id == user.EmployeeId);
                if (employee == null)
                {
                    throw new InvalidOperationException($"Could not find the employee with the specified id: {user.EmployeeId}");
                }


                result = new AppIdentityUser
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    EmployeeId = user.EmployeeId,
                    UserCulture = user.UserCulture,
                    SecurityStamp = user.SecurityStamp,
                    ConcurrencyStamp = user.ConcurrencyStamp,
                    AccessFailedCount = user.AccessFailedCount,
                    EmailConfirmed = user.EmailConfirmed,
                    LockoutEnabled = user.LockoutEnabled,
                    NormalizedEmail = user.NormalizedEmail,
                    PasswordHash = user.PasswordHash,
                    LockoutEnd = user.LockoutEnd,
                    NormalizedUserName = user.NormalizedUserName,
                    PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                    PhoneNumber = user.PhoneNumber,
                    TwoFactorEnabled = user.TwoFactorEnabled,
                    FirstName = employee.Name,
                    LastName = employee.Surname,
                    MiddleName = employee.MiddleName!,
                    IsActive = user.IsActive,
                    FcmToken = DataHelpers.SafeString(user.FcmToken)
                };
            }
            return result;
        }

        public override async Task<IList<string>> GetRolesAsync(AppIdentityUser user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var userId = user.Id;
            var query = from userRole in Context.AspNetUserRoles
                        join role in Context.AspNetRoles on userRole.RoleId equals role.Id
                        where userRole.UserId.Equals(userId)
                        select role.Name;

            return await query.ToListAsync(cancellationToken);
        }
    }
}
