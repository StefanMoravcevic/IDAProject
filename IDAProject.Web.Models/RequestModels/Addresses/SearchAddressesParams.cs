using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.RequestModels.Addresses
{
    public class SearchAddressesParams
    {
        public int? Id { get; set; }
        public int? BenefitUserId { get; set; }
        public int? PartnerId { get; set; }
        public int? CompanyId { get; set; }
        public string? Keyword { get; set; }
    }
}
