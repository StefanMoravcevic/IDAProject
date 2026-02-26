namespace IDAProject.Web.Models.General.Enums
{
    public static class PartnerTypes
    {
        public const int Customers = 12;
        public const int Lessees = 10;
        public const int Suppliers = 11;
        public const int Subcontractors = 4;
        public const int Brokers = 5;
        public const int Factoring_houses = 6;
        public const int Bank = 2;
        public const int Insurance = 8;
        public const int ELD = 9;
        public const int FM = 1;
        public const int Fund = 3;

        public const int Others = 9999;

        public static string GetDescription(int partnerType)
        {
            switch (partnerType)
            {
                case Customers:
                    return "Customers";
                case Lessees:
                    return "Leases";
                case Suppliers:
                    return "Suppliers";
                case Subcontractors:
                    return "Renters & Subcontractors";
                case Brokers:
                    return "Brokers";
                case Factoring_houses:
                    return "Factoring Houses";
                case Bank:
                    return "Banks";
                case FM:
                    return "FM";
                case Fund:
                    return "Fund";
                case Insurance:
                    return "Insurance";
                case ELD:
                    return "ELD";
                case Others:
                    return "Others";
                default:
                    return string.Empty;
            }
        }
    }
}