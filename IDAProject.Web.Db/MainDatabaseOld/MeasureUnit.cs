using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class MeasureUnit
    {
        public MeasureUnit()
        {
            GeneralSettingMeasureFuels = new HashSet<GeneralSetting>();
            GeneralSettingMeasureTraveledWays = new HashSet<GeneralSetting>();
            GeneralSettingMeasureVehicleLengths = new HashSet<GeneralSetting>();
            GeneralSettingMeasureVehicleWeights = new HashSet<GeneralSetting>();
            MaintenanceServices = new HashSet<MaintenanceService>();
            ServiceItems = new HashSet<ServiceItem>();
        }

        public int Id { get; set; }
        public string Sign { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int MeasureUnitTypeId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual MeasureUnitType MeasureUnitType { get; set; } = null!;
        public virtual ICollection<GeneralSetting> GeneralSettingMeasureFuels { get; set; }
        public virtual ICollection<GeneralSetting> GeneralSettingMeasureTraveledWays { get; set; }
        public virtual ICollection<GeneralSetting> GeneralSettingMeasureVehicleLengths { get; set; }
        public virtual ICollection<GeneralSetting> GeneralSettingMeasureVehicleWeights { get; set; }
        public virtual ICollection<MaintenanceService> MaintenanceServices { get; set; }
        public virtual ICollection<ServiceItem> ServiceItems { get; set; }
    }
}
