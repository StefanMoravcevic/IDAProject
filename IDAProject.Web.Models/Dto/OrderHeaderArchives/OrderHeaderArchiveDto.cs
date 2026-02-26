using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.Dto.OrderHeaderArchives
{
    public class OrderHeaderArchiveDto : SaveOrderHeaderArchiveRequestModel
    {
        public OrderHeaderArchiveDto()
        {
        }
        #region Basic data

        
        public string? CustomerOrderNumber { get; set; }
        public string? CreatedDateFormatted
        {
            get
            {
                return CreatedDate.HasValue
                    ? CreatedDate.Value.ToString("dd.MM.yyyy")
                    : null;
            }
        }

        #endregion
    }
}
