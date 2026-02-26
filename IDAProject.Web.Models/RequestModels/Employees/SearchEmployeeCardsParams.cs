namespace IDAProject.Web.Models.RequestModels.Employees
{
    public class SearchEmployeeCardsParams
    {
        public int? Id { get; set; } 
        public int? EmployeeId { get; set; } 
        public int? CardTypeId { get; set; } 
        public int? CardId { get; set; } 
        public string? Date { get; set; }
        public string? CardNumber { get; set; }

    }
}
