using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.Dto.PackagingOrders
{
	public class PackagingOrderLists
	{
		public List<PackagingOrderDto> SalesPackagingOrder { get; set; }
		public List<PackagingOrderDto> TransferPackagingOrder { get; set; }
		public List<PackagingOrderDto> PersonalPackagingOrder { get; set; }
		public List<PackagingOrderDto> TransferAdditionPackagingOrder { get; set; }
	}
}
