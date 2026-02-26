using IDAProject.Web.Helpers;

namespace IDAProject.Web.Models.Dto.Partners
{
    public class SubcontractorFeeDto : SaveSubcontractorFeeRequestModel
    {
        public SubcontractorFeeDto()
        {
        }


        public string DateFromFormatted
        {
            get { return DisplayFormatHelpers.FormatDate(DateFrom); }
        }

        public string DateToFormatted
        {
            get { return DisplayFormatHelpers.FormatDate(DateTo); }
        }

        public string RecordDateFormatted
        {
            get { return DisplayFormatHelpers.FormatDate(RecordDate); }
        }

        public string FeeFormatted
        {
            get
            {
                var fee = DisplayFormatHelpers.FormatDecimal(Fee);
                return $"{fee} %";
            }
        }

    }
}
