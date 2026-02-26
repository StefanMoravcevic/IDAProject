namespace IDAProject.Web.Models.Dto.Partners
{
    public class SaveSubcontractorFeeRequestModel
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int SubcontractorCompanyId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public decimal Fee { get; set; }
        public DateTime RecordDate { get; set; }
    }
}
