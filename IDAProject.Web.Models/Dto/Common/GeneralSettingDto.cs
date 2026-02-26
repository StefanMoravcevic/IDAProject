namespace IDAProject.Web.Models.Dto.Common
{
    public class GeneralSettingDto : SaveGeneralSettingRequestModel
    {
        public GeneralSettingDto()
        {
            MeasureTraveledWay = new MeasureUnitDto();
            MeasureVehicleLength = new MeasureUnitDto();
            MeasureVehicleWeight = new MeasureUnitDto();
            MeasureVehicleVolume = new MeasureUnitDto();
			MeasureFuel = new MeasureUnitDto();
            Currency = new currency();
            DateFormat = string.Empty;
            DecimalPlaces = 2;
        }

        public MeasureUnitDto MeasureVehicleLength { get; set; }
        public MeasureUnitDto MeasureVehicleWeight { get; set; }
		public MeasureUnitDto MeasureVehicleVolume { get; set; }
		public MeasureUnitDto MeasureFuel { get; set; }
        public MeasureUnitDto MeasureTraveledWay { get; set; }
        public currency Currency { get; set; }
    }
}
