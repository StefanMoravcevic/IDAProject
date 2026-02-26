namespace IDAProject.Web.Models.Common
{
    public class AvailabilityIssue
    {
        public AvailabilityIssue()
        {
            Type = string.Empty;
            Reason = string.Empty;
            Critical = false;
        }

        public string Type { get; set; }

        public string Reason { get; set; }

        public bool Critical { get; set; }
    }
}
