using IDAProject.Web.Helpers;

namespace IDAProject.Web.Models.Dto.Partners
{
    public class FactoringFeeDto : SaveFactoringFeeRequestModel
    {
        public FactoringFeeDto()
        {

        }

        #region Basic data

        
        public string? PartnerName { get; set; }
        public string? CompanyName { get; set; }
        public string RecordDateFormatted
        {
            get { return DisplayFormatHelpers.FormatDate(RecordDate); }
        }
        public string DateFromFormatted
        {
            get { return DisplayFormatHelpers.FormatDate(DateFrom); }
        }
        public string DateToFormatted
        {
            get { return DisplayFormatHelpers.FormatDate(DateTo); }
        }
        public string FeeFormatted
        {
            get { return DisplayFormatHelpers.FormatDecimal(Fee); }
        }

        #endregion

    }
}
