namespace IDAProject.Web.Models.Dto.DocumentSeries
{
    public class SaveDocumentSerieRequestModel
    {
        public int Id { get; set; }
        public string? Pattern { get; set; }
        public int NextNumber { get; set; }
        public int IncrementSeed { get; set; }
        public int DocumentSerieTypeId { get; set; }
        public int Year { get; set; }
    }
}