using System.Reflection;

namespace IDAProject.Web.Api.Reports.ReportHelpers
{
    internal static class InternalHelpers
    {
        public static Stream GetEmbederReportDefinitionNyName(string templateFileName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var stream = assembly.GetManifestResourceStream($"IDAProject.Web.Api.Reports.Templates.{templateFileName}");
            return stream!;
        }
    }
}
