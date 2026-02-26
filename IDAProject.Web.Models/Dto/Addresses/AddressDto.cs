using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.Dto.Addresses
{
    public class AddressDto : SaveAddressRequestModel
    {
        public AddressDto()
        {
        }
        #region Basic data

        public string? StreetName { get; set; }
        public string? StreetNumber { get; set; }
        public string? ZipCode { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? BenefitUser { get; set; }
        public string? Partner { get; set; }
        public string? Company { get; set; }


        #endregion
    }
}
