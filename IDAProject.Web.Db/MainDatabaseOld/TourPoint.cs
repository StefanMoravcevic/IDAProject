using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class TourPoint
    {
        public TourPoint()
        {
            TourClaims = new HashSet<TourClaim>();
            TourPointSpecialInstructions = new HashSet<TourPointSpecialInstruction>();
        }

        public int Id { get; set; }
        public int TourId { get; set; }
        public int TypeId { get; set; }
        public int StatusId { get; set; }
        public int PointNumber { get; set; }
        public string CompanyName { get; set; } = null!;
        public DateTime RecordDateTime { get; set; }
        public string? ZipCode { get; set; }
        public string Address { get; set; } = null!;
        public int CityId { get; set; }
        public int StateId { get; set; }
        public string? ContactName { get; set; }
        public string? Phone { get; set; }
        public DateTime TimeFrom { get; set; }
        public DateTime TimeTo { get; set; }
        public int TimeZoneId { get; set; }
        public string? Note { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual City City { get; set; } = null!;
        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual State State { get; set; } = null!;
        public virtual TourStatus Status { get; set; } = null!;
        public virtual TimeZone TimeZone { get; set; } = null!;
        public virtual Tour Tour { get; set; } = null!;
        public virtual TourPointType Type { get; set; } = null!;
        public virtual ICollection<TourClaim> TourClaims { get; set; }
        public virtual ICollection<TourPointSpecialInstruction> TourPointSpecialInstructions { get; set; }
    }
}
