using Microsoft.EntityFrameworkCore;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Db.MainDatabase;
using IDAProject.Web.Helpers;
using IDAProject.Web.Models.Dto.Printers;
using IDAProject.Web.Models.RequestModels.Printers;

namespace IDAProject.Web.Api.Repositories
{
    public class PrintersRepository : IPrintersRepository
    {
        private readonly IDAProjectContext _dbContext;

        public PrintersRepository(IDAProjectContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PrinterDto> GetPrinterByIdAsync(int id)
        {
            var searchParams = new SearchPrintersParams
            {
                Id = id
            };
            var result = await SearchPrintersAsync(searchParams);
            return result.FirstOrDefault();
        }

        public async Task<List<PrinterDto>> SearchPrintersAsync(SearchPrintersParams searchParams)
        {

            var result = new List<PrinterDto>();
            IQueryable<Printer> query = _dbContext.Printers.Where(x => x.IsDeleted == false);
            if (searchParams.Id.HasValue)
            {
                query = query.Where(x => x.Id == searchParams.Id);
            }
            else
            {
                if (!string.IsNullOrEmpty(searchParams.BarCode))
                {
                    query = query.Where(x => x.BarCode == searchParams.BarCode);
                }
            }

            result = await query.Select(a => new PrinterDto
            {
               
                Id = a.Id,
                Name = a.Name,
                BarCode = a.BarCode,
                Ip4Address = a.Ip4Address,
                Port = a.Port


            }).ToListAsync();
            return result;

        }

        public async Task<int> SavePrinterAsync(SavePrinterRequestModel requestModel)
        {
            Printer? dbRecord;
            if (requestModel.Id > 0)
            {
                dbRecord = await _dbContext.Printers.SingleAsync(x => x.Id == requestModel.Id);
                DataHelpers.CopyObjectWithIL(requestModel, dbRecord);

            }
            else
            {
                dbRecord = DataHelpers.CloneObjectWithIL<SavePrinterRequestModel,Printer>(requestModel);
                _dbContext.Printers.Add(dbRecord!);
            }
            await _dbContext.SaveChangesAsync();
            return dbRecord!.Id;
        }

        public async Task DeletePrinterAsync(int id, int? userId)
        {
            var dbRecord = await _dbContext.Printers.SingleAsync(x => x.Id == id);
            dbRecord.IsDeleted = true;
            dbRecord.DeletedBy = userId;
            dbRecord.DeletedDate = DateTime.Now;
            _dbContext.Printers.Update(dbRecord);
            await _dbContext.SaveChangesAsync();
        }

    }
}
    