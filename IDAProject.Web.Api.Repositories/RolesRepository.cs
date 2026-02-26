using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Db.MainDatabase;
using IDAProject.Web.Helpers;
using IDAProject.Web.Models.Auth.RequestModels;
using IDAProject.Web.Models.Dto.Roles;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.General.Enums;
using IDAProject.Web.Models.RequestModels.Roles;

namespace IDAProject.Web.Api.Repositories
{
    public class RolesRepository : IRolesRepository
    {
        private readonly IDAProjectContext _dbContext;

        public RolesRepository(IDAProjectContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<RoleDto?> GetRoleByIdAsync(int id)
        {
            var searchParams = new SearchRolesParams
            {
                Id = id
            };

            var result = await SearchRolesAsync(searchParams);
            return result.FirstOrDefault();
        }

        public async Task<int> SaveRoleAsync(SaveRoleRequestModel requestModel)
        {
            AspNetRole? dbRecord;
            var keyNormalizer = new UpperInvariantLookupNormalizer();
            if (requestModel.Id > 0)
            {
                dbRecord = await _dbContext.AspNetRoles.SingleAsync(x => x.Id == requestModel.Id);
                dbRecord.NormalizedName = keyNormalizer.NormalizeName(requestModel.Name);
                dbRecord.ConcurrencyStamp = keyNormalizer.NormalizeName(Guid.NewGuid().ToString());

                DataHelpers.CopyObjectWithIL(requestModel, dbRecord);
            }
            else
            {
                dbRecord = DataHelpers.CloneObjectWithIL<SaveRoleRequestModel, AspNetRole>(requestModel);
                dbRecord!.NormalizedName = keyNormalizer.NormalizeName(requestModel.Name);
                dbRecord.ConcurrencyStamp = keyNormalizer.NormalizeName(Guid.NewGuid().ToString());
                _dbContext.AspNetRoles.Add(dbRecord!);
            }

            await _dbContext.SaveChangesAsync();
            return dbRecord!.Id;
        }

        public async Task<List<RoleDto>> SearchRolesAsync(SearchRolesParams searchParams)
        {
            var result = new List<RoleDto>();
            var query = from p in _dbContext.AspNetRoles
                        select new RoleDto
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Company = p.Company!.Name,
                            CompanyId = p.CompanyId
                        };

            if (searchParams.Id.HasValue)
            {
                query = query.Where(x => x.Id == searchParams.Id);
            }
            else
            {
                if (!string.IsNullOrEmpty(searchParams.Keyword))
                {
                    var pattern = $"%{searchParams.Keyword}%";
                    query = query.Where(x =>
                    EF.Functions.Like(x.Name!, pattern));
                }
            }

            result = await query.ToListAsync();

            return result;
        }

        public async Task DeleteRoleAsync(int id, int? userId)
        {
            var dbRecord = await _dbContext.AspNetRoles.SingleAsync(x => x.Id == id);

            //dbRecord.IsDeleted = true;
            //dbRecord.DeletedBy = userId;
            //dbRecord.DeletedDate = DateTime.Now;
            _dbContext.AspNetRoles.Remove(dbRecord);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<int> CreateRoleFeatureAsync(CreateRoleFeatureModel requestModel)
        {
            var roleFeature = new AspNetRoleFeature
            {
                AspNetFeatureId = requestModel.FeatureId,
                AspNetRoleId = requestModel.RoleId
            };
            _dbContext.AspNetRoleFeatures.Add(roleFeature);
            await _dbContext.SaveChangesAsync();
            return roleFeature.Id;
        }
        public async Task<int> DeleteRoleFeatureAsync(int roleFeatureId)
        {

            var dbUserRole = await _dbContext.AspNetRoleFeatures.SingleAsync(x => x.Id == roleFeatureId);
            if (dbUserRole == null)
            {
                throw new InvalidOperationException($"Role feature with id: {roleFeatureId} not found.");
            }

            _dbContext.AspNetRoleFeatures.Remove(dbUserRole);
            await _dbContext.SaveChangesAsync();
            return roleFeatureId;
        }
        public async Task<bool> SearchRoleFeatureAsync(CreateRoleFeatureModel requestModel)
        {
            var dbRoleFeature = await _dbContext.AspNetRoleFeatures.FirstOrDefaultAsync(x => x.AspNetFeatureId == requestModel.FeatureId && x.AspNetRoleId == requestModel.RoleId);
            if (dbRoleFeature != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<List<RoleFeatureDto>> GetRoleFeaturesByRoleIdAsync(int id)
        {
            var result = new List<RoleFeatureDto>();
            var query = from r in _dbContext.AspNetRoleFeatures
                        select new RoleFeatureDto
                        {
                            Id = r.Id,
                            RoleId = r.AspNetRoleId,
                            FeatureName = r.AspNetFeature.Name!
                        };

            if (id > 0)
            {
                query = query.Where(x => x.RoleId == id);
            }
            result = await query.ToListAsync();
            return result;
        }


    }
}