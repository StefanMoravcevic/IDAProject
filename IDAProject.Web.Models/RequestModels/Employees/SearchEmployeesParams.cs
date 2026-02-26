
namespace IDAProject.Web.Models.RequestModels.Employees
{
    public class SearchEmployeesParams
    {
        public SearchEmployeesParams()
        {
            JobTypes = new List<int>();
        }

        public SearchEmployeesParams(int jobType) : this()
        {
            JobTypes.Add(jobType);
        }

        public int? Id { get; set; } 
        public bool? Blocked { get; set; }
        public string? Keyword { get; set; }
        public int? Active { get; set; }
        public int? CommpanyId { get; set; }
		public int? OrgUnitId { get; set; }

		public List<int> JobTypes { get; set; }
        public int? JobTypeId { get; set; }
    }
}
