using System.Globalization;

namespace IDAProject.Web.Helpers
{
    public static class MasterDataConverter
    {
        public const string TrueBoolValue = "true";

        public static object? GetRawValue(string value, Type targetType)
        {
            if (targetType == typeof(string))
            {
                return value;
            }
            else if (targetType == typeof(int))
            {
                return int.Parse(value, NumberStyles.Integer, CultureInfo.InvariantCulture);
            }
            else if (targetType == typeof(int?))
            {
                int result;
                if (int.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out result))
                {
                    return result;
                }
                return new int?();
            }
            else if (targetType == typeof(bool))
            {
                return string.Equals(value, TrueBoolValue, StringComparison.OrdinalIgnoreCase);
            }
            else if (targetType == typeof(bool?))
            {
                if (string.IsNullOrEmpty(value))
                {
                    return new bool?();
                }
                return string.Equals(value, TrueBoolValue, StringComparison.OrdinalIgnoreCase);
            }
            else if (targetType == typeof(long))
            {
                return long.Parse(value, NumberStyles.Integer, CultureInfo.InvariantCulture);
            }
            else if (targetType == typeof(long?))
            {
                long result;
                if (long.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out result))
                {
                    return result;
                }
                return new long?();
            }
            else if (targetType == typeof(float))
            {
                return float.Parse(value, NumberStyles.Number, CultureInfo.InvariantCulture);
            }
            else if (targetType == typeof(float?))
            {
                float result;
                if (float.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out result))
                {
                    return result;
                }
                return new float?();
            }
            else if (targetType == typeof(double))
            {
                return double.Parse(value, NumberStyles.Number, CultureInfo.InvariantCulture);
            }
            else if (targetType == typeof(double?))
            {
                double result;
                if (double.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out result))
                {
                    return result;
                }
                return new double?();
            }
            else if (targetType == typeof(decimal))
            {
                return decimal.Parse(value, NumberStyles.Number, CultureInfo.InvariantCulture);
            }
            else if (targetType == typeof(decimal?))
            {
                return DataHelpers.GetRequestValueDecimal(value);
            }
            else if (targetType == typeof(DateTime))
            {
                return DateTime.Parse(value, CultureInfo.InvariantCulture, DateTimeStyles.None);
            }
            else if (targetType == typeof(DateTime?))
            {
                DateTime result;
                if (DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                {
                    return result;
                }
                return new DateTime?();
            }

            throw new NotImplementedException($"Not implemented target type: {targetType}");
        }

        public static object? GetRawValue(string value, string targetType)
        {
            var type = Type.GetType(targetType);
            return GetRawValue(value, type!);
        }

        public static string SerializeRawValue(object? value, Type valueType)
        {
            if (valueType == typeof(string))
            {
                return (value as string)!;
            }
            else if (valueType == typeof(int))
            {
                return ((int)value!).ToString("F0", CultureInfo.InvariantCulture);
            }
            else if (valueType == typeof(int?))
            {
                var v = value as int?;
                if(v.HasValue)
                {
                    return SerializeRawValue(v.Value, typeof(int));
                }
                return string.Empty;
            }
            else if (valueType == typeof(bool))
            {
                return ((bool)value!) ? "true" : "false";
            }
            else if (valueType == typeof(bool?))
            {
                var v = value as bool?;
                if (v.HasValue)
                {
                    return SerializeRawValue(v.Value, typeof(bool));
                }
                return string.Empty;
            }
            else if (valueType == typeof(long))
            {
                return ((long)value!).ToString("F0", CultureInfo.InvariantCulture);
            }
            else if (valueType == typeof(long?))
            {
                var v = value as long?;
                if (v.HasValue)
                {
                    return SerializeRawValue(v.Value, typeof(long));
                }
                return string.Empty;
            }
            else if (valueType == typeof(float))
            {
                return ((float)value!).ToString("N2", CultureInfo.InvariantCulture);
            }
            else if (valueType == typeof(float?))
            {
                var v = value as float?;
                if (v.HasValue)
                {
                    return SerializeRawValue(v.Value, typeof(float));
                }
                return string.Empty;
            }
            else if (valueType == typeof(double))
            {
                return ((double)value!).ToString("N4", CultureInfo.InvariantCulture);
            }
            else if (valueType == typeof(double?))
            {
                var v = value as double?;
                if (v.HasValue)
                {
                    return SerializeRawValue(v.Value, typeof(double));
                }
                return string.Empty;
            }
            else if (valueType == typeof(decimal))
            {
                return ((decimal)value!).ToString("N4", CultureInfo.InvariantCulture);
            }
            else if (valueType == typeof(decimal?))
            {
                var v = value as decimal?;
                if (v.HasValue)
                {
                    return SerializeRawValue(v.Value, typeof(decimal));
                }
                return string.Empty;
            }
            else if (valueType == typeof(DateTime))
            {
                return ((DateTime)value!).ToString(CultureInfo.InvariantCulture);
            }
            else if (valueType == typeof(DateTime?))
            {
                var v = value as decimal?;
                if (v.HasValue)
                {
                    return SerializeRawValue(v.Value, typeof(DateTime));
                }
                return string.Empty;
            }

            throw new NotImplementedException($"Not implemented target type: {valueType}");
        }

        public static string SerializeRawValue(object? value)
        {
            if(value == null)
            {
                return string.Empty;
            }
            var result = SerializeRawValue(value, value!.GetType());
            return result;
        }
    }
}