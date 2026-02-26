using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAProject.Web.Helpers;

namespace IDAProject.Web.Models.Dto.ScannedLines
{
    public class ScannedLineDto : SaveScannedLineRequestModel
    {
        public ScannedLineDto()
        {
        }
        #region Basic data

        public string? FebiArticleNo { get; set; }
        public string? CustomerOrderNumber { get; set; }


        public string? DateFormatted
        {
            get
            {
                return Date.HasValue
                    ? Date.Value.ToString("dd.MM.yyyy")
                    : null;
            }
        }

        #endregion
    }
}
