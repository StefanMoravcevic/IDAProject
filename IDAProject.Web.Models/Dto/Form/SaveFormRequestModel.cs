using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.Dto.Form
{
    public class SaveFormRequestModel
    {
        public int Id { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }
        public int? DocumentTypeId { get; set; }
        public string? TemplateFile { get; set; }
    }
}
