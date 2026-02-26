using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.Dto.PackagesStatus
{
    public class SavePackageStatusRequestModel
    {
        public string? CustomerName { get; set; }

        public string? SourceDocumentNo { get; set; }

        public int? Status { get; set; }

        public string? LocationCode { get; set; }
    }
}
