using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.Dto.ScannedLines
{
    public class SaveScannedLineRequestModel
    {
        public Int32 Id { get; set; }
public Int32? ScannedQuantity { get; set; }
public Int32? RequestedQuantity { get; set; }
public Int32? OrderLineId { get; set; }
public DateTime? Date { get; set; }
public Int32? UserId { get; set; }

    }
}
