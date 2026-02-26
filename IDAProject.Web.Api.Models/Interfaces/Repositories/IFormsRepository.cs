using IDAProject.Web.Models.Dto.Form;
using IDAProject.Web.Models.RequestModels.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Api.Models.Interfaces.Repositories
{
    public interface IFormsRepository
    {

        Task<List<FormDto>> SearchFormsAsync(SearchFormsParams searchParams);
        Task<int> SaveFormAsync(SaveFormRequestModel requestModel);
        Task DeleteFormAsync(int id, int? userId);
        Task<FormDto> GetFormByIdAsync(int id);
        
    }
}
