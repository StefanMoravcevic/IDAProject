using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.Dto.ShippingStatus
{
	public class ShippingLists
	{
		public List<ShippingStatusDto> ShippingList { get; set; }
		public List<ShippingStatusDto> PersonalTakeofList { get; set; }
	}
}
