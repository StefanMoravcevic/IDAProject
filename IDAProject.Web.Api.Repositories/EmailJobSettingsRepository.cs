using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Helpers;
using IDAProject.Web.Models.Dto.EmailJobSettings;
using IDAProject.Web.Models.General.Enums;
using IDAProject.Web.Models.RequestModels.EmailJobSettings;
using Microsoft.EntityFrameworkCore;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Db.MainDatabase;
using IDAProject.Web.Helpers;
using IDAProject.Web.Models.Dto.EmailJobSettings;
using IDAProject.Web.Models.General.Enums;
using IDAProject.Web.Models.RequestModels.EmailJobSettings;

namespace IDAProject.Web.Api.Repositories
{
    public class EmailJobSettingsRepository : IEmailJobSettingsRepository
    {
        private readonly IDAProject.Web.Db.MainDatabase.IDAProjectContext _dbContext;

        public EmailJobSettingsRepository(IDAProject.Web.Db.MainDatabase.IDAProjectContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task DeleteEmailJobSettingAsync(int id, int? userId)
        {
            var dbRecord = await _dbContext.EmailJobSettings.SingleAsync(x => x.Id == id);
            dbRecord.IsDeleted = true;
            dbRecord.DeletedBy = userId;
            dbRecord.DeletedDate = DateTime.Now;
            _dbContext.EmailJobSettings.Update(dbRecord);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<EmailJobSettingDto>> GetEmailJobSettingsAsync(EmailJobTypes emailJobType)
        {
            var query = from gs in _dbContext.EmailJobSettings
                        where gs.EmailJobTypeId == (int)emailJobType
                        select new EmailJobSettingDto()
                        {
                            Id = gs.Id,
                            EmailJobTypeId = gs.EmailJobTypeId,
                            Note = gs.Note,
                            Email1 = gs.Email1,
                            Email2 = gs.Email2,
                            Email3 = gs.Email3,
                            Email4 = gs.Email4,
                            Email5 = gs.Email5,
                            Email6 = gs.Email6,
                            Email7 = gs.Email7,
                            Email8 = gs.Email8,
                            Email9 = gs.Email9,
                            Email10 = gs.Email10,
                            Enabled = Convert.ToBoolean(gs.Enabled)
                        };

            var result = await query.ToListAsync();

            return result;
        }

        public async Task<EmailJobSettingDto> GetEmailJobSettingsByIdAsync(int id)
        {
            var searchParams = new SearchEmailJobSettingsParams
            {
                Id = id
            };
            var result = await SearchEmailJobSettingsAsync(searchParams);
            return result.FirstOrDefault()!;
        }

        public async Task<int> SaveEmailJobSettingsAsync(EmailJobSettingDto requestModel)
        {
            EmailJobSetting? dbRecord;
            if (requestModel.Id > 0)
            {
                dbRecord = await _dbContext.EmailJobSettings.SingleAsync(x => x.Id == requestModel.Id);
                DataHelpers.CopyObjectWithIL(requestModel, dbRecord);

            }
            else
            {
                dbRecord = DataHelpers.CloneObjectWithIL<EmailJobSettingDto, EmailJobSetting>(requestModel);
                _dbContext.EmailJobSettings.Add(dbRecord!);
            }
            await _dbContext.SaveChangesAsync();
            return dbRecord!.Id;
        }

        public async Task<List<EmailJobSettingDto>> SearchEmailJobSettingsAsync(SearchEmailJobSettingsParams searchParams)
        {
            var result = new List<EmailJobSettingDto>();
            IQueryable<EmailJobSetting> query = _dbContext.EmailJobSettings.Where(x => x.IsDeleted == false).OrderByDescending(x => x.Id);
            if (searchParams.Id.HasValue)
            {
                query = query.Where(x => x.Id == searchParams.Id);
            }
            else
            {

            }

            result = await (from ebc in query
                            select new EmailJobSettingDto
                            {
                                Id = ebc.Id,
                                Email1 = ebc.Email1,
                                Email2 = ebc.Email2,
                                Email3 = ebc.Email3,
                                Email4 = ebc.Email4,
                                Email5 = ebc.Email5,
                                Email6 = ebc.Email6,
                                Email7 = ebc.Email7,
                                Email8 = ebc.Email8,
                                Email9 = ebc.Email9,
                                Email10 = ebc.Email10,
                                EmailJobTypeId = ebc.EmailJobTypeId,
                                Enabled = ebc.Enabled,
                                Type = ebc.EmailJobType!.Name,
                                Note = ebc.Note

                            }).OrderByDescending(x => x.Id).ToListAsync();

            return result;
        }
    }
}
