namespace IDAProject.Web.Models.Dto.Partners
{
    public class SaveFactoringFeeRequestModel
    {
        public SaveFactoringFeeRequestModel()
        {
            DateFrom = DateTime.Now.Date;
            DateTo = DateTime.Now.Date;
        }
        public int Id { get; set; }
        public int? CompanyId { get; set; }
        public int FactoringCompanyId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public DateTime RecordDate { get; set; }
        public decimal Fee { get; set; }
    }
}
