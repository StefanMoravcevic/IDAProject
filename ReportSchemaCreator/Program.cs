using ReportSchemaCreator;
using System.Reflection;
using System.Text;

// App environement variables

var currentAssembly = Assembly.GetExecutingAssembly();
var asmFileInfo = new FileInfo(currentAssembly.Location);
var parentFolder = asmFileInfo.Directory;

while (parentFolder != null)
{
    parentFolder = parentFolder.Parent;
    if (string.Equals(parentFolder!.Name, "src", StringComparison.OrdinalIgnoreCase))
    {
        break;
    }
}

if(parentFolder == null)
{
    throw new InvalidOperationException("Invalid project structure. Root source folder: [src] is missing.");
}

var destinationFolder = Path.Combine(parentFolder!.FullName, "IDAProject.Web.Api.Reports", "Schemas");

// End of App environement variables

//Tools.WriteSchema<InvoiceReportModel>(destinationFolder);
//Tools.WriteSchema<StatementReportModel>(destinationFolder);
//Tools.WriteSchema<DriverCardReportModel>(destinationFolder);