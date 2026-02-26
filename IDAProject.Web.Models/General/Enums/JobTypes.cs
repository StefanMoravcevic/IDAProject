namespace IDAProject.Web.Models.General.Enums
{
    public static class JobTypes
    {
        public const int Company_driver = 1;
        public const int Dispatcher = 2;
        public const int External_driver = 3;
        public const int CEO = 4;
        public const int Administration = 5;
        public const int Other = 9999;

        public static string GetDescription(int jobTypeId)
        {
            switch (jobTypeId)
            {
                case Company_driver:
                    return "Company driver";
                case Dispatcher:
                    return "Dispatcher";
                case External_driver:
                    return "External driver";
                case CEO:
                    return "CEO";
                case Administration:
                    return "Administration";
                default:
                    return "Other";
            }
        }
    }
}
