namespace IDAProject.Web.Models.Dto.Messages
{
    public class ContactInfo
    {
        public ContactInfo()
        {
            Id = 0;
            FullName = string.Empty;
            Role = string.Empty;
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
    }
}