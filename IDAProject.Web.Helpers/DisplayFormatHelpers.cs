using System.Globalization;

namespace IDAProject.Web.Helpers
{
    public static class DisplayFormatHelpers
    {
        private static readonly CultureInfo _culture;
        private static readonly NumberFormatInfo GeolocationFormat;

        static DisplayFormatHelpers()
        {
            _culture = new CultureInfo("en-US");

            GeolocationFormat = new NumberFormatInfo
            {
                NumberDecimalSeparator = ".",
                NumberGroupSeparator = "",
                PercentDecimalSeparator = ".",
                PercentGroupSeparator = "",
                CurrencyDecimalSeparator = ".",
                CurrencyGroupSeparator = ""
            };
        }

        public static string FormatDate(DateTime? date)
        {
            var result = string.Empty;
            if (date.HasValue)
            {
                result = date.Value.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            }
            return result;
        }

        public static string FormatDateTime(DateTime? date)
        {
            var result = string.Empty;
            if (date.HasValue)
            {
                result = date.Value.ToString("MM/dd/yyyy hh:mm tt", CultureInfo.InvariantCulture);
            }
            return result;
        }

        public static string FormatDateTimeRange(DateTime timeFrom, DateTime timeTo, string timeZone)
        {
            var timeFromFormatted = FormatDateTime(timeFrom);
            var timeToFormatted = FormatDateTime(timeTo);
            if (timeFrom.Date == timeTo.Date)
            {
                return timeFromFormatted + " - " + FormatTime(timeTo) + " " + timeZone;
            }

            return timeFromFormatted + " - " + timeToFormatted + " " + timeZone;
        }

        public static string FormatDateTimeRange(DateTime timeFrom, DateTime timeTo, string timeZoneFrom, string timeZoneTo)
        {
            var timeFromFormatted = FormatDateTime(timeFrom);
            var timeToFormatted = FormatDateTime(timeTo);
            if (timeFrom.Date == timeTo.Date)
            {
                return timeFromFormatted + " - " + FormatTime(timeTo) + " " + timeZoneFrom;
            }

            if (string.Equals(timeZoneFrom, timeZoneTo, StringComparison.OrdinalIgnoreCase))
            {
                return $"{timeFromFormatted} - {timeToFormatted} {timeZoneFrom}";
            }

            return $"{timeFromFormatted} {timeZoneFrom} - {timeToFormatted} {timeZoneTo}";
        }

        public static string FormatTime(DateTime? date)
        {
            var result = string.Empty;
            if (date.HasValue)
            {
                result = date.Value.ToString("hh:mm tt", CultureInfo.InvariantCulture);
            }
            return result;
        }

        public static string FormatDecimal(decimal? value)
        {
            var result = string.Empty;
            if (value.HasValue)
            {
                result = value.Value.ToString("N2", _culture);
            }
            return result;
        }

        public static string FormatDecimalAdaptive(decimal? value)
        {
            var result = string.Empty;
            if (value.HasValue)
            {
                var has1DecimalPlace = value.Value % 1 > 0;

                if (has1DecimalPlace)
                {
                    var testVal = value.Value * 10m;
                    var has2DecimalPlaces = testVal % 1 > 0;

                    if (has2DecimalPlaces)
                    {
                        result = value.Value.ToString("N2", _culture);
                    }
                    else
                    {
                        result = value.Value.ToString("N1", _culture);
                    }
                }
                else
                {
                    result = value.Value.ToString("N0", _culture);
                }
            }
            return result;
        }

        public static string FormatAsGeolocationMapCoordinate(double value)
        {
            var result = value.ToString("N6", GeolocationFormat);
            return result;
        }
    }
}