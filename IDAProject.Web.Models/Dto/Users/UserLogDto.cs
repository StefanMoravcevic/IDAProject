namespace IDAProject.Web.Models.Dto.Users
{
	public class UserLogDto
    {
		public int Id { get; set; }
        public int? AspNetUserId { get; set; }
        public DateTime? LoginDateTime { get; set; }
        public DateTime? LogoutDateTime { get; set; }
        public string? UserName { get; set; }
        public string? WindowsUserName { get; set; }
        public string? LocalIp { get; set; }
        public string? RemoteIp { get; set; }
        public string? PublicIp { get; set; }
        public int? LocalPort { get; set; }
        public int? RemotePort { get; set; }
        public string? Note { get; set; }

    }
}
