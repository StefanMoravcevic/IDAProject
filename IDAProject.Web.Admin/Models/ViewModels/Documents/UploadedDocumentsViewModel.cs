using IDAProject.Web.Admin.Models.Html.AjaxTable;
using Microsoft.Extensions.Localization;

namespace IDAProject.Web.Admin.Models.ViewModels.Documents
{
    public class UploadedDocumentsViewModel : NavigationWithAjaxTableViewModel

    {
        private readonly IStringLocalizer<SharedResources> _localizer;

        public UploadedDocumentsViewModel(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            Columns = new List<ColumnDefinition>
            {
                //new ColumnDefinition("DocumentType", "Document type"),
                new ColumnDefinition("DownloadFileName", _localizer["DownloadFileName"]) { OnPrepareCellContent = "getDocumentDownloadLink" },
                new ColumnDefinition("UploadedBy", _localizer["Uploaded by"]),
                new ColumnDefinition("UploadedDateFormatted", _localizer["Uploaded date"]),
                new ColumnDefinition()
                {
                    IsOptionsColumn = true,
                    Options = new List<ColumnOption>
                    {
                        {
                            new ColumnOption("delete")
                            {
                                FontIconClass = "fa-trash-o",
                                Color = "black"
                            }
                        }
                    },
                    HeaderStyle = "width:60px;"
                }
            };
        }
    }
}
