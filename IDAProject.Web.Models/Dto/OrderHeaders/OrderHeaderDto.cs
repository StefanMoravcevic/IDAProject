using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.Dto.OrderHeaders
{
    public class OrderHeaderDto : SaveOrderHeaderRequestModel
    {
        public OrderHeaderDto()
        {
        }
        #region Basic data

        public string? CreatedDateFormatted
        {
            get
            {
                return CreatedDate.HasValue
                    ? CreatedDate.Value.ToString("dd.MM.yyyy")
                    : null;
            }
        }

        //public string? CreatedTimeFormatted
        //{
        //    get
        //    {
        //        return CreatedTime.HasValue
        //            ? CreatedTime.Value.ToString("HH:mm")
        //            : null;
        //    }
        //}

        #endregion
    }
}
