using System.Text;

namespace ReportSchemaCreator
{
    internal static class Tools
    {
        public static void WriteSchema<T>(string destinationFolder)
        {
            var reportSourceType = typeof(T);
            var xri = new System.Xml.Serialization.XmlReflectionImporter();
            var xss = new System.Xml.Serialization.XmlSchemas();
            var xse = new System.Xml.Serialization.XmlSchemaExporter(xss);

            var xtm = xri.ImportTypeMapping(reportSourceType);
            xse.ExportTypeMapping(xtm);

            var schemaFilePath = Path.Combine(destinationFolder, $"{reportSourceType.Name}Schema.xsd");

            using var sw = new StreamWriter(schemaFilePath, false, Encoding.UTF8);
            for (int i = 0; i < xss.Count; i++)
            {
                var xs = xss[i];
                xs.Id = $"{reportSourceType.Name}Schema";
                xs.Write(sw);
            }
        }
    }
}