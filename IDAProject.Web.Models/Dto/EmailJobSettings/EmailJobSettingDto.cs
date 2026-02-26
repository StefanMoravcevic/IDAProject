namespace IDAProject.Web.Models.Dto.EmailJobSettings
{
    public class EmailJobSettingDto
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public int? EmailJobTypeId { get; set; }

        public string? Email1 { get; set; }

        public string? Email2 { get; set; }

        public string? Email3 { get; set; }

        public string? Email4 { get; set; }

        public string? Email5 { get; set; }

        public string? Email6 { get; set; }

        public string? Email7 { get; set; }

        public string? Email8 { get; set; }

        public string? Email9 { get; set; }

        public string? Email10 { get; set; }

        public string? Note { get; set; }

        public bool Enabled { get; set; }

        public string? Type { get; set; }

        public string EmailString
        {
            get { return string.Join(",", new List<string>() { Email1, Email2, Email3, Email4, Email5, Email6, Email7, Email8, Email9, Email10 }.Where(x => !string.IsNullOrEmpty(x))); }
        }
    }
}
