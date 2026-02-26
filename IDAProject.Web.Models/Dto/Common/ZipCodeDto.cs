namespace IDAProject.Web.Models.Dto.Common
{
    public class ZipCodeDto
    {
        public int Id { get; set; }
        public string ZipCode1 { get; set; } = null!;
        public string ZipCodeType { get; set; } = null!;
        public string CityType { get; set; } = null!;
        public string CountyFips { get; set; } = null!;
        public string StateFips { get; set; } = null!;
        public string MsaCode { get; set; } = null!;
        public string AreaCode { get; set; } = null!;
        public string TimeZone { get; set; } = null!;
        public int Utc { get; set; }
        public string Dst { get; set; } = null!;
    }
}
