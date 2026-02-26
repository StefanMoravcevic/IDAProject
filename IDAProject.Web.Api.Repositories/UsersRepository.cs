using Microsoft.EntityFrameworkCore;
using System.Data;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Db.MainDatabase;
using IDAProject.Web.Helpers;
using IDAProject.Web.Models.Dto.Users;
using IDAProject.Web.Models.RequestModels.Users;
using IDAProject.Web.Models.Auth.RequestModels;

namespace IDAProject.Web.Api.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IDAProjectContext _dbContext;

        public UsersRepository(IDAProjectContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            var response = new UserDto();
            var dbRecord = await _dbContext.AspNetUsers.FirstOrDefaultAsync(x => x.Id == id);
            DataHelpers.CopyObjectWithIL(dbRecord, response);

            return response;
        }

        public async Task<List<UserRoleDto>> GetRolesByUserIdAsync(int id)
        {
            var result = new List<UserRoleDto>();
            IQueryable<AspNetUserRole> query = _dbContext.AspNetUserRoles;
            query = query.Where(x => x.UserId == id);
            result = await (from usrrols in query
                            select new UserRoleDto
                            {
                                Id = usrrols.Id,
                                UserUsername = usrrols.User.UserName,
                                RoleName = usrrols.Role.Name,
                                UserId = usrrols.UserId,
                                RoleId = usrrols.RoleId

                            }).ToListAsync();

            return result;
            ;
        }
        public async Task<List<UserDto>> GetUsersByRoleIdAsync(int roleId)
        {
            var result = new List<UserDto>();
            IQueryable<AspNetUserRole> query = _dbContext.AspNetUserRoles;
            query = query.Where(x => x.User.IsActive == true && x.RoleId == roleId);
            result = await (from usrrols in query
                            select new UserDto
                            {
                                Id = usrrols.UserId,
                                UserName = usrrols.User.UserName,
                                Email = usrrols.User.Email,
                                UserCulture = usrrols.User.UserCulture!,
                                EmployeeId = usrrols.User.EmployeeId

                            }).ToListAsync();

            return result;
            ;
        }

        public async Task<List<UserTableItemDto>> SearchUsersAsync(SearchUsersParams searchParams)
        {
            var result = new List<UserTableItemDto>();
            IQueryable<AspNetUser> query = _dbContext.AspNetUsers;

            if (!string.IsNullOrEmpty(searchParams.Keyword))
            {
                query = query.Where(x => x.UserName.Contains(searchParams.Keyword) || x.Email.Contains(searchParams.Keyword) || x.PhoneNumber!.Contains(searchParams.Keyword) || x.Employee.Name.Contains(searchParams.Keyword) || x.Employee.Surname.Contains(searchParams.Keyword));
            }

            if (searchParams.Active.HasValue)
            {
                if (searchParams.Active == 1)
                    query = query.Where(x => x.IsActive == true);
                else if (searchParams.Active == 0)
                    query = query.Where(x => x.IsActive == false);
                if (searchParams.EmployeeId.HasValue)
                {
                    query = query.Where(x => x.EmployeeId == searchParams.EmployeeId);
                }

                if (searchParams.OrgUnitId.HasValue)
                {
                    query = query.Where(x => x.Employee.OrgUnitId == searchParams.OrgUnitId);
                }

                if (searchParams.OrgUnitIdUser.HasValue)
                {
                    query = query.Where(x => x.OrgId == searchParams.OrgUnitIdUser);
                }
            }

            result = await (from usr in query
                            select new UserTableItemDto
                            {
                                Id = usr.Id,
                                UserName = usr.UserName,
                                Employee = usr.Employee.Name + ' ' + usr.Employee.Surname,
                                PartnerName = usr.Partner.Name,
                                PhoneNumber = usr.PhoneNumber,
                                Email = usr.Email,
                                IsActive = usr.IsActive,
                                EmailConfirmed = usr.EmailConfirmed,
                                PhoneNumberConfirmed = usr.PhoneNumberConfirmed,
                                UserCulture = usr.UserCulture,
                                NumberOfRoles = usr.AspNetUserRoles.Count,
                                Roles = string.Join(", ", usr.AspNetUserRoles.Select(x => x.Role.Name))
                                

                            }).ToListAsync();

            return result;
        }

        public async Task UpdateUserAsync(SaveUserRequestModel requestModel)
        {

            var user = _dbContext.AspNetUsers.Single(x => x.Id == requestModel.Id);
            user.Email = requestModel.Email;
            user.PhoneNumber = requestModel.PhoneNumber;
            user.IsActive = requestModel.IsActive;
            user.PhoneNumberConfirmed = requestModel.PhoneNumberConfirmed;
            user.EmailConfirmed = requestModel.EmailConfirmed;
            user.EmployeeId = requestModel.EmployeeId;
            user.PartnerId = requestModel.PartnerId;
            user.OrgId = requestModel.OrgId;
            user.NormalizedEmail = requestModel.NormalizedEmail;
            user.UserCulture = requestModel.UserCulture;
            user.ConcurrencyStamp = Guid.NewGuid().ToString();
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateUsersPasswordAsync(int userId, string newPasswordHash)
        {

            var user = _dbContext.AspNetUsers.Single(x => x.Id == userId);
            user.PasswordHash = newPasswordHash;
            user.ConcurrencyStamp = Guid.NewGuid().ToString();
            await _dbContext.SaveChangesAsync();
        }

        public async Task<int> CreateUserRoleAsync(CreateUserRoleModel requestModel)
        {
            var userRole = new AspNetUserRole
            {
                UserId = requestModel.UserId,
                RoleId = requestModel.RoleId
            };
            _dbContext.AspNetUserRoles.Add(userRole);
            await _dbContext.SaveChangesAsync();
            return userRole.Id;
        }

        public async Task<int> DeleteUserRoleAsync(int userRoleId)
        {
            var dbUserRole = await _dbContext.AspNetUserRoles.SingleAsync(x => x.Id == userRoleId);
            if (dbUserRole == null)
            {
                throw new InvalidOperationException($"User role with id: {userRoleId} not found.");
            }

            _dbContext.AspNetUserRoles.Remove(dbUserRole);
            await _dbContext.SaveChangesAsync();
            return userRoleId;
        }
        public async Task<bool> SearchUserRoleAsync(CreateUserRoleModel requestModel)
        {
            var dbUserRole = await _dbContext.AspNetUserRoles.FirstOrDefaultAsync(x => x.UserId == requestModel.UserId && x.RoleId == requestModel.RoleId);
            if (dbUserRole != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<int> GetUserCompanyIdAsync(int idUser)
        {
            var query = from u in _dbContext.AspNetUsers
                        where u.Id == idUser
                        select u.Employee.CompanyId;

            var result = await query.FirstOrDefaultAsync();
            return result.Value;
        }

        public async Task<string> GetUserInitialsAsync(int idUser)
        {
            var query = from u in _dbContext.AspNetUsers
                        where u.Id == idUser
                        select new { u.Employee.Name, u.Employee.Surname };

            var emp = await query.FirstOrDefaultAsync();

            var result = string.Empty;

            if (emp != null && emp.Name.Length > 0 && emp.Surname.Length > 0)
            {
                result = emp.Name.Substring(0, 1) + emp.Surname.Substring(0, 1);
            }
            return result;
        }

        public async Task<string> GetUserFullNameAsync(int idUser)
        {
            var query = from u in _dbContext.AspNetUsers
                        where u.Id == idUser
                        select u.Employee.Name + " " + u.Employee.Surname;


            var result = await query.FirstOrDefaultAsync();

            if (result == null)
            {
                result = string.Empty;
            }
            return result;
        }


        public async Task<int> SaveUserLogAsync(SaveUserLogRequestModel requestModel)
        {
            UserLog? dbRecord;
            if (requestModel.Id > 0)
            {
                dbRecord = await _dbContext.UserLogs.SingleAsync(x => x.Id == requestModel.Id);
                DataHelpers.CopyObjectWithIL(requestModel, dbRecord);
            }
            else
            {
                dbRecord = DataHelpers.CloneObjectWithIL<SaveUserLogRequestModel, UserLog>(requestModel);
                _dbContext.UserLogs.Add(dbRecord!);
            }
            await _dbContext.SaveChangesAsync();
            return dbRecord!.Id;
        }

        public async Task<List<UserLogDto>> SearchUserLogsAsync(SearchUserLogsParams searchParams)
        {
            var result = new List<UserLogDto>();
            IQueryable<UserLog> query = _dbContext.UserLogs;

            if (searchParams.UserId.HasValue)
            {
                query = query.Where(x => x.AspNetUserId == searchParams.UserId);
            }

            result = await (from usrLog in query
                            select new UserLogDto
                            {
                                Id = usrLog.Id,
                                UserName = usrLog.UserName,
                                Note = usrLog.Note,
                                AspNetUserId = usrLog.AspNetUserId,
                                LocalIp = usrLog.LocalIp,
                                LocalPort = usrLog.LocalPort,
                                LoginDateTime = usrLog.LoginDateTime,
                                LogoutDateTime = usrLog.LogoutDateTime,
                                PublicIp = usrLog.PublicIp,
                                WindowsUserName = usrLog.WindowsUserName,
                                RemoteIp = usrLog.RemoteIp,
                                RemotePort = usrLog.RemotePort
                            }).ToListAsync();

            return result;
        }

    }
}