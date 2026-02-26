using IDAProject.Web.Helpers;

namespace IDAProject.Web.Models.Dto.Partners
{
    public class PartnerDto : SavePartnerRequestModel
    {
        public PartnerDto()
        {

        }

        #region Basic data

        
        public string? ZipCode { get; set; }
        public string? PaymentCondition { get; set; }
        public string? PrimaryContact { get; set; }
        public string? IncomeType { get; set; }
        public string? PartnerType { get; set; }
        public string? PartnerCategorie { get; set; }
        //public int PartnerCategoryId { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? StateShort { get; set; }
        public string? ContactCompany { get; set; }
        
        //public string BirthDateFormatted
        //{
        //    get { return DisplayFormatHelpers.FormatDate(BirthDate); }
        //}

        #endregion

    }
}
