using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class TourCrew
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public int TypeId { get; set; }
        public int DriverId { get; set; }
        public int? CoDriverId { get; set; }
        public int? DispatcherId { get; set; }
        public decimal? LoadedMiles { get; set; }
        public decimal? EmptyMiles { get; set; }
        public int VehicleId { get; set; }
        public int? TrailerId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual Employee? CoDriver { get; set; }
        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual Employee? Dispatcher { get; set; }
        public virtual Employee Driver { get; set; } = null!;
        public virtual Tour Tour { get; set; } = null!;
        public virtual Vehicle? Trailer { get; set; }
        public virtual TourCrewType Type { get; set; } = null!;
        public virtual Vehicle Vehicle { get; set; } = null!;
    }
}
