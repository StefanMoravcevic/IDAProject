using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.Dto.OrderLineArchives
{
    public class OrderLineArchiveDto : SaveOrderLineArchiveRequestModel
    {
        public OrderLineArchiveDto()
        {
        }
        #region Basic data

        public string? FebiArticleNo { get; set; }
        public string? FebiArticleName { get; set; }
        public int? FebiArticlePackingUnit { get; set; }
        public string? CustomerOrderNumber { get; set; }
        public string? TourName { get; set; }
        public string? WintArticleNo { get; set; }
        public string? OrderDateFormatted
        {
            get
            {
                return OrderDate.HasValue
                    ? OrderDate.Value.ToString("dd.MM.yyyy HH:mm")
                    : null;
            }
        }

        #endregion
    }
}
