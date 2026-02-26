using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class VehicleEldHistory
    {
        public VehicleEldHistory()
        {
            VehicleEldData = new HashSet<VehicleEldDatum>();
        }

        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int EldNoteId { get; set; }
        public int EldPartnerId { get; set; }
        public int? CompanyId { get; set; }
        public string? Eldnumber { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string? Note { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual Company? Company { get; set; }
        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual EldDisconnectedNote EldNote { get; set; } = null!;
        public virtual Partner EldPartner { get; set; } = null!;
        public virtual Vehicle Vehicle { get; set; } = null!;
        public virtual ICollection<VehicleEldDatum> VehicleEldData { get; set; }
    }
}
