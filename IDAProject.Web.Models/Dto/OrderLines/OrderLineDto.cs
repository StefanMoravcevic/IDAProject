using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAProject.Web.Helpers;

namespace IDAProject.Web.Models.Dto.OrderLines
{
    public class OrderLineDto : SaveOrderLineRequestModel
    {
        public OrderLineDto()
        {
        }
        #region Basic data

        public string? CustomerOrderNumber { get; set; }
        public string? FebiArticleNo { get; set; }
        public string? FebiArticleName { get; set; }
        public string? Options { get; set; }
        public string? WintArticleNo { get; set; }
        public int? FebiArticlePackingUnit { get; set; }
        
        public string? TourName { get; set; }
        public string? OrderDateFormatted
        {
            get
            {
                return OrderDate.HasValue
                    ? OrderDate.Value.ToString("dd.MM.yyyy HH:mm")
                    : null;
            }
        }

        public string StatusColor
        {
            get
            {
                if (CheckedQuantity == 0)
                    return "gray";
                else if (CheckedQuantity < RequestedQuantity)
                    return "red";
                else
                    return "green";
            }
        }

        #endregion
    }
}
