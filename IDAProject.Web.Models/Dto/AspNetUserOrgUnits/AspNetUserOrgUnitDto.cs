using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.Dto.AspNetUserOrgUnits
{
    public class AspNetUserOrgUnitDto : SaveAspNetUserOrgUnitRequestModel
    {
        public AspNetUserOrgUnitDto()
        {
        }
        #region Basic data
         
        public string? AspNetUser { get; set; } 
        public string? OrgUnit { get; set; } 
        

        #endregion
    }
}
