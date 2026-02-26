using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class Vehicle
    {
        public Vehicle()
        {
            Credits = new HashSet<Credit>();
            Deductions = new HashSet<Deduction>();
            DriverStopBonuses = new HashSet<DriverStopBonuse>();
            FuelConsumptions = new HashSet<FuelConsumption>();
            Loans = new HashSet<Loan>();
            MaintenancePlans = new HashSet<MaintenancePlan>();
            ServiceDocuments = new HashSet<ServiceDocument>();
            Statements = new HashSet<Statement>();
            Tolls = new HashSet<Toll>();
            TourCrewTrailers = new HashSet<TourCrew>();
            TourCrewVehicles = new HashSet<TourCrew>();
            VehicleAdvanceCosts = new HashSet<VehicleAdvanceCost>();
            VehicleAndEquipmentHistories = new HashSet<VehicleAndEquipmentHistory>();
            VehicleAndTrailersHistoryTrailers = new HashSet<VehicleAndTrailersHistory>();
            VehicleAndTrailersHistoryTrucks = new HashSet<VehicleAndTrailersHistory>();
            VehicleAnnualDotinspections = new HashSet<VehicleAnnualDotinspection>();
            VehicleAssignments = new HashSet<VehicleAssignment>();
            VehicleEldHistories = new HashSet<VehicleEldHistory>();
            VehicleFmcsainspections = new HashSet<VehicleFmcsainspection>();
            VehicleIftasettings = new HashSet<VehicleIftasetting>();
            VehicleIncomes = new HashSet<VehicleIncome>();
            VehicleMaintenanceCosts = new HashSet<VehicleMaintenanceCost>();
            VehicleOptionalCosts = new HashSet<VehicleOptionalCost>();
            VehiclePasses = new HashSet<VehiclePass>();
            VehiclePaymentPlans = new HashSet<VehiclePaymentPlan>();
            VehicleRegistrations = new HashSet<VehicleRegistration>();
            VehicleRentContracts = new HashSet<VehicleRentContract>();
            VehicleServiceStatuses = new HashSet<VehicleServiceStatus>();
            VehicleStatusHistories = new HashSet<VehicleStatusHistory>();
            Violations = new HashSet<Violation>();
        }

        public int Id { get; set; }
        public string InternalNumber { get; set; } = null!;
        public int VehicleModelId { get; set; }
        public int? OwnerId { get; set; }
        public int CompanyId { get; set; }
        public int? OrgUnitId { get; set; }
        public int OwnershipTypeId { get; set; }
        public string ChassisNumber { get; set; } = null!;
        public DateTime VehicleYear { get; set; }
        public DateTime? FirstRegistrationYear { get; set; }
        public DateTime? AcquisitionDate { get; set; }
        public DateTime? SaleDate { get; set; }
        public string? Color { get; set; }
        public string LicensePlate { get; set; } = null!;
        public DateTime? PlatesExpirationDate { get; set; }
        public int RegistrationStateId { get; set; }
        public string? VehicleNote { get; set; }
        public int? LoadCapacity { get; set; }
        public int? RegisteredGrossWeight { get; set; }
        public string? BoxSize { get; set; }
        public int? InternalWidth { get; set; }
        public int? InternalDoorHeight { get; set; }
        public int? Height { get; set; }
        public int? Width { get; set; }
        public bool HasLoadingRamp { get; set; }
        public bool HasSleepingCabin { get; set; }
        public bool Active { get; set; }
        public string? EngineType { get; set; }
        public string? OilType { get; set; }
        public bool ManualTransmission { get; set; }
        public int IdlingHours { get; set; }
        public bool HasAuxiliaryPowerUnit { get; set; }
        /// <summary>
        /// 1 - Diesel, 2 - Petrol
        /// </summary>
        public int? FuelType { get; set; }
        public string? TyreSizeSteeringAxel { get; set; }
        public string? TyreSizeDriveAxel { get; set; }
        public string? TyreSizeTrailerAxel { get; set; }
        public int? MileageAtTimeOfPurchase { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual Company Company { get; set; } = null!;
        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual OrgUnit? OrgUnit { get; set; }
        public virtual Partner? Owner { get; set; }
        public virtual VehicleOwnershipType OwnershipType { get; set; } = null!;
        public virtual VehicleModel VehicleModel { get; set; } = null!;
        public virtual ICollection<Credit> Credits { get; set; }
        public virtual ICollection<Deduction> Deductions { get; set; }
        public virtual ICollection<DriverStopBonuse> DriverStopBonuses { get; set; }
        public virtual ICollection<FuelConsumption> FuelConsumptions { get; set; }
        public virtual ICollection<Loan> Loans { get; set; }
        public virtual ICollection<MaintenancePlan> MaintenancePlans { get; set; }
        public virtual ICollection<ServiceDocument> ServiceDocuments { get; set; }
        public virtual ICollection<Statement> Statements { get; set; }
        public virtual ICollection<Toll> Tolls { get; set; }
        public virtual ICollection<TourCrew> TourCrewTrailers { get; set; }
        public virtual ICollection<TourCrew> TourCrewVehicles { get; set; }
        public virtual ICollection<VehicleAdvanceCost> VehicleAdvanceCosts { get; set; }
        public virtual ICollection<VehicleAndEquipmentHistory> VehicleAndEquipmentHistories { get; set; }
        public virtual ICollection<VehicleAndTrailersHistory> VehicleAndTrailersHistoryTrailers { get; set; }
        public virtual ICollection<VehicleAndTrailersHistory> VehicleAndTrailersHistoryTrucks { get; set; }
        public virtual ICollection<VehicleAnnualDotinspection> VehicleAnnualDotinspections { get; set; }
        public virtual ICollection<VehicleAssignment> VehicleAssignments { get; set; }
        public virtual ICollection<VehicleEldHistory> VehicleEldHistories { get; set; }
        public virtual ICollection<VehicleFmcsainspection> VehicleFmcsainspections { get; set; }
        public virtual ICollection<VehicleIftasetting> VehicleIftasettings { get; set; }
        public virtual ICollection<VehicleIncome> VehicleIncomes { get; set; }
        public virtual ICollection<VehicleMaintenanceCost> VehicleMaintenanceCosts { get; set; }
        public virtual ICollection<VehicleOptionalCost> VehicleOptionalCosts { get; set; }
        public virtual ICollection<VehiclePass> VehiclePasses { get; set; }
        public virtual ICollection<VehiclePaymentPlan> VehiclePaymentPlans { get; set; }
        public virtual ICollection<VehicleRegistration> VehicleRegistrations { get; set; }
        public virtual ICollection<VehicleRentContract> VehicleRentContracts { get; set; }
        public virtual ICollection<VehicleServiceStatus> VehicleServiceStatuses { get; set; }
        public virtual ICollection<VehicleStatusHistory> VehicleStatusHistories { get; set; }
        public virtual ICollection<Violation> Violations { get; set; }
    }
}
