using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.Dto.AspNetUserOrgUnits
{
    public class SaveAspNetUserOrgUnitRequestModel
    {
        public Int32 Id { get; set; }
public Int32? AspNetUserId { get; set; }
public Int32? OrgUnitId { get; set; }

    }
}
