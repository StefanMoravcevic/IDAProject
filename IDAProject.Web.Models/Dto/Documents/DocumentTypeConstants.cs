namespace IDAProject.Web.Models.Dto.Documents
{
    public static class DocumentTypeConstants
    {
        /// <summary>
        /// BOL - Bill Of Lading
        /// </summary>
        public const int Tour_Loaded = 1;
        public const int Tour_Pickup_SealNumber = 2;
        public const int Tour_Delivered = 3;
        public const int Fmcsa_Inspection = 4;
        public const int Adot_Inspection = 5;
        public const int Tour = 6;
        public const int Driver_license = 7;
        public const int Certificate = 8;
        public const int Other_documents = 9;
        public const int Message_image = 10;
        public const int Email_Queue_Attachment = 11;
        public const int Employee_Document = 12;
        public const int Tour_RateOfConfirmation = 13;
        public const int VehicleAssignment_Condition = 14;

        /// <summary>
        /// POD - Proof Of Delivery
        /// </summary>
        public const int Tour_ProofOfDelivery = 15;
    }
}