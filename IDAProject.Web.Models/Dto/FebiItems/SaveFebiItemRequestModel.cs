using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.Dto.FebiItems
{
    public class SaveFebiItemRequestModel
    {
        public Int32 Id { get; set; }
public String FebiArticleNo { get; set; }
public String FebiArticleName { get; set; }
public Int32? FebiPackingUnit { get; set; }
        public string? BarCode { get; set; }
        public string? WintArticleNo { get; set; }

    }
}
