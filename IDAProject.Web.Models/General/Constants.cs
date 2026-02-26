namespace IDAProject.Web.Models.General
{
    public class Constants
    {
        public const string ApiKeyName = "api_key";

        public static string  AdminCookieToken = "admin_token";
        public static int SessionDurationInDays = 10;
        public static int ShortLivedTokenExpirationInMinutes = 10; //changed to minutes
        public static int LongLivedTokenExpirationInDays = 10;

        public const string ClaimFeature = "ClaimFeature";
        public const string ClaimEmployeeId = "ClaimEmployeeId";
        public const string ClaimPartnerId = "ClaimPartnerId";
        public const string ClaimOrgId = "ClaimOrgId";
        public const string ClaimPrinterId = "ClaimPrinterId";
        public const string ClaimUserCulture = "ClaimUserCulture";
        public const string ClaimEmployeeCompanyName = "ClaimEmployeeCompanyName";
        public const string ClaimPartnerName = "ClaimPartnerName";
        public const string FcmToken = "FcmToken";

        public const string TableSettings_HiddenColumns = "hc";
        public const string TableSettings_ColumnsOrder = "co";

        public const int GeneralSettingsId = 1;

        public const string NOK = "nok";
        public static string SuccessHeaderKey = "_shk_";

        #region Content types (MIME types)

        public static readonly string ContentType_Png = "image/png";
        public static readonly string ContentType_Jpg = "image/jpeg";
        public static readonly string ContentType_Pdf = "application/pdf";
        public static readonly string ContentType_Xlsx = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        public static readonly string ContentType_Xls = "application/vnd.ms-excel";
        public static readonly string ContentType_Txt = "text/plain";
        public static readonly string ContentType_Xml = "application/xml";
        public static readonly string ContentType_XmlT = "text/xml";

        #endregion

    }
}
