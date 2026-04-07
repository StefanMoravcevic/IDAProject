using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAProject.Web.Helpers;

namespace IDAProject.Web.Models.Dto.Projects
{
    public class ProjectDto : SaveProjectRequestModel
    {
        public ProjectDto()
        {
        }
        #region Basic data

        public string? DueDateFormatted
        {
            get { return DisplayFormatHelpers.FormatDate(DueDate); }
        }

        #endregion
    }
}
