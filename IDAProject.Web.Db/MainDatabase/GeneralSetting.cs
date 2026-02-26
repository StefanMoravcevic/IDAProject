using System;
using System.Collections.Generic;

namespace IDAProject.Web.Db.MainDatabase;

public partial class GeneralSetting
{
    public int Id { get; set; }

    public int MeasureVehicleLengthId { get; set; }

    public int MeasureVehicleWeightId { get; set; }

    public int MeasureFuelId { get; set; }

    public int MeasureTraveledWayId { get; set; }

    public int CurrencyId { get; set; }

    public string DateFormat { get; set; } = null!;

    public int DecimalPlaces { get; set; }

    public int? ReminderDaysDriverLicense { get; set; }

    public int? ReminderDaysLicensePlate { get; set; }

    public int? ReminderDaysSafetyTest { get; set; }

    public int? ReminderDaysAdot { get; set; }

    public int? ReminderDaysIfta { get; set; }

    public int? ReminderEmployeeCertificates { get; set; }

    public int? ReminderAdvanceCosts { get; set; }

    public int? ReminderMaintenancePlan { get; set; }

    public int? ReminderMaintenanceMileage { get; set; }

    public int? ReminderMaintenanceWorkingHours { get; set; }

    public bool EmployeeGroupedView { get; set; }

    public bool VehicleGroupedView { get; set; }

    public string? LocationCode { get; set; }

    public string? MessageOfTheDay { get; set; }

    public string? LeftBanner { get; set; }

    public string? RightBanner { get; set; }

    public string? FullPageAd { get; set; }

    public virtual Currency Currency { get; set; } = null!;

    public virtual MeasureUnit MeasureFuel { get; set; } = null!;

    public virtual MeasureUnit MeasureTraveledWay { get; set; } = null!;

    public virtual MeasureUnit MeasureVehicleLength { get; set; } = null!;

    public virtual MeasureUnit MeasureVehicleWeight { get; set; } = null!;
}
