using System;
using System.Collections.Generic;

namespace IDAProject.Web.Db.MainDatabase;

public partial class MeasureUnit
{
    public int Id { get; set; }

    public string Sign { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int MeasureUnitTypeId { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedDate { get; set; }

    public int? DeletedBy { get; set; }

    public virtual AspNetUser? DeletedByNavigation { get; set; }

    public virtual ICollection<GeneralSetting> GeneralSettingMeasureFuels { get; set; } = new List<GeneralSetting>();

    public virtual ICollection<GeneralSetting> GeneralSettingMeasureTraveledWays { get; set; } = new List<GeneralSetting>();

    public virtual ICollection<GeneralSetting> GeneralSettingMeasureVehicleLengths { get; set; } = new List<GeneralSetting>();

    public virtual ICollection<GeneralSetting> GeneralSettingMeasureVehicleWeights { get; set; } = new List<GeneralSetting>();

    public virtual MeasureUnitType MeasureUnitType { get; set; } = null!;
}
