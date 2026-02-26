
namespace IDAProject.Web.Models.Dto.Companies
{
    public class CompanyDto : SaveCompanyRequestModel
    {
        public CompanyDto()
        {
        }
        #region Basic data


        public string? ParentCompany { get; set; }
        public string? FactoringHouse { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }

        #endregion

    }
}
