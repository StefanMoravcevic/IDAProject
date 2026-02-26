using System.Reflection.Emit;
using System.Reflection;
using System.Globalization;
using System.Net.Mail;
using System.Collections.ObjectModel;

namespace IDAProject.Web.Helpers
{
    public static class DataHelpers
    {
        /// <summary>
        /// This dictionary caches the delegates for each 'to-clone' type.
        /// </summary>
        private static readonly Dictionary<string, Delegate> CachedIl;

        private static object _cachedDictionarySyncRoot;

        private static NullabilityInfoContext _nullabilityInfoContext;

        static DataHelpers()
        {
            CachedIl = new Dictionary<string, Delegate>();
            _cachedDictionarySyncRoot = new object();
            _nullabilityInfoContext = new NullabilityInfoContext();
        }

        public static byte[] GetBytes(string str)
        {
            var bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string SafeString(object? value)
        {
            var result = string.Empty;

            if (value != null)
            {
                if (value is string)
                {
                    var strVal = value as string;
                    if (strVal != null)
                    {
                        result = strVal;
                    }
                }
                else
                {
                    result = value.ToString();
                }
            }
            return result!;
        }

        public static string SafeTrim(object? value)
        {
            var result = SafeString(value);
            return result.Trim();
        }

        public static string SafeSubstring(string value, int length)
        {
            if (value == null)
            {
                return string.Empty;
            }
            if (value.Length < length)
            {
                return value;
            }

            return value.Substring(0, length);
        }

        public static int? SafeInt(object val)
        {
            var result = new int?();
            if (val is string strVal)
            {
                if (!string.IsNullOrEmpty(strVal))
                {
                    int parsedInt;
                    if (int.TryParse(strVal, NumberStyles.None, CultureInfo.InvariantCulture, out parsedInt))
                    {
                        result = parsedInt;
                    }
                }
            }
            else
            {
                try
                {
                    result = Convert.ToInt32(val);
                }
                catch { /* silent exception */ }
            }
            return result;
        }

        /// <summary>
        /// Generic cloning method that clones an object using IL.
        /// Only the first call of a certain type will hold back performance.
        /// After the first call, the compiled IL is executed.
        /// </summary>
        /// <typeparam name="T">Type of object to clone</typeparam>
        /// <param name="myObject">Object to clone</param>
        /// <returns>Cloned object</returns>
        public static T CloneObjectWithIL<T>(T myObject)
        {
            Delegate myExec;

            var key = $"clone-simple-{typeof(T).Name}-->{typeof(T).Name}";

            lock (_cachedDictionarySyncRoot)
            {
                if (!CachedIl.TryGetValue(key, out myExec!))
                {
                    myExec = CreateCloneObjectWithILDelegate<T>(myObject);
                    CachedIl.Add(key, myExec);
                }
            }
            var function = myExec as Func<T, T>;
            var result = function(myObject);
            return result;
        }

        private static Delegate CreateCloneObjectWithILDelegate<T>(T myObject)
        {
            // Create ILGenerator
            var dymMethod = new DynamicMethod("DoClone", typeof(T), new Type[] { typeof(T) }, true);
            var cInfo = myObject!.GetType().GetConstructor(new Type[] { });

            var generator = dymMethod.GetILGenerator();

            generator.DeclareLocal(typeof(T));

            generator.Emit(OpCodes.Newobj, cInfo!);
            generator.Emit(OpCodes.Stloc_0);
            foreach (FieldInfo field in myObject.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                // Load the new object on the eval stack... (currently 1 item on eval stack)
                generator.Emit(OpCodes.Ldloc_0);
                // Load initial object (parameter)          (currently 2 items on eval stack)
                generator.Emit(OpCodes.Ldarg_0);
                // Replace value by field value             (still currently 2 items on eval stack)
                generator.Emit(OpCodes.Ldfld, field);
                // Store the value of the top on the eval stack into the object underneath that value on the value stack.
                //  (0 items on eval stack)
                generator.Emit(OpCodes.Stfld, field);
            }

            // Load new constructed obj on eval stack -> 1 item on stack
            generator.Emit(OpCodes.Ldloc_0);
            // Return constructed object.   --> 0 items on stack
            generator.Emit(OpCodes.Ret);

            var result = dymMethod.CreateDelegate(typeof(Func<T, T>));
            return result;
        }


        /// <summary>
        /// Generic cloning method that clones an object using IL.
        /// Only the first call of a certain type will hold back performance. After the first call, the compiled IL is executed.
        /// </summary>
        /// <typeparam name="In">Type of source object to clone</typeparam>
        /// <typeparam name="Out">Type of destination object to copy values</typeparam>
        /// <param name="myObject">Sourc e object</param>
        /// <returns></returns>
        public static Out? CloneObjectWithIL<In, Out>(In myObject) where Out : class
        {
            if (myObject == null)
            {
                return null;
            }

            Delegate myExec;
            var key = $"clone-{typeof(In).Name}-->{typeof(Out).Name}";

            lock (_cachedDictionarySyncRoot)
            {
                if (!CachedIl.TryGetValue(key, out myExec!))
                {
                    myExec = CreateCloneObjectWithILDelegate<In, Out>();
                    CachedIl.Add(key, myExec);
                }
            }

            var function = myExec as Func<In, Out>;
            var result = function(myObject);
            return result;
        }

        private static Delegate CreateCloneObjectWithILDelegate<In, Out>()
        {
            // Create ILGenerator
            var dymMethod = new DynamicMethod("CloneFromOneTypeToAnother", typeof(Out), new Type[] { typeof(In) }, true);
            var cInfo = typeof(Out).GetConstructor(new Type[] { });

            var generator = dymMethod.GetILGenerator();

            generator.DeclareLocal(typeof(Out));

            generator.Emit(OpCodes.Newobj, cInfo!);
            generator.Emit(OpCodes.Stloc_0);

            var srcFields = GetFieldsIncludingBaseClasses(typeof(In), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            var destFields = GetFieldsIncludingBaseClasses(typeof(Out), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (FieldInfo destField in destFields)
            {
                var srcField = srcFields.FirstOrDefault(x => x.Name == destField.Name);
                if (srcField != null && srcField.FieldType == destField.FieldType)
                {
                    // Load the new object on the eval stack... (currently 1 item on eval stack)
                    generator.Emit(OpCodes.Ldloc_0);
                    // Load initial object (parameter)          (currently 2 items on eval stack)
                    generator.Emit(OpCodes.Ldarg_0);
                    // Replace value by field value             (still currently 2 items on eval stack)
                    generator.Emit(OpCodes.Ldfld, srcField);
                    // Store the value of the top on the eval stack into the object underneath that value on the value stack.
                    //  (0 items on eval stack)
                    generator.Emit(OpCodes.Stfld, destField);
                }
            }

            // Load new constructed obj on eval stack -> 1 item on stack
            generator.Emit(OpCodes.Ldloc_0);
            // Return constructed object.   --> 0 items on stack
            generator.Emit(OpCodes.Ret);

            // In case of State to StateDto it generates the following method:
            // public static StateDto CloneFromOneTypeToAnother(State P_0)
            // {
            //    StateDto stateDto = new StateDto();
            //    stateDto.Id = P_0.Id;
            //    stateDto.Name = P_0.Name;
            //    stateDto.ShortName = P_0.ShortName;
            //    return stateDto;
            // }

            var result = dymMethod.CreateDelegate(typeof(Func<In, Out>));
            return result;
        }

        public static void CopyObjectWithIL<In, Out>(In source, Out target)
        {
            Delegate myExec;

            var sourceType = typeof(In);
            var targetType = typeof(Out);
            var key = $"copy-{sourceType.Name}-->{targetType.Name}";

            lock (_cachedDictionarySyncRoot)
            {
                if (!CachedIl.TryGetValue(key, out myExec!))
                {
                    myExec = CreateCopyObjectWithILDelegate<In, Out>();
                    CachedIl.Add(key, myExec);
                }
            }

            var action = myExec as Action<In, Out>;
            action(source, target);
        }

        public static Delegate CreateCopyObjectWithILDelegate<In, Out>()
        {
            var sourceType = typeof(In);
            var targetType = typeof(Out);
            var key = $"copy-{sourceType.Name}-->{targetType.Name}";

            // Create ILGenerator
            var dymMethod = new DynamicMethod("CopyObject", null, new Type[] { sourceType, targetType }, true);

            var generator = dymMethod.GetILGenerator();
            // used to avoid .locals init in the method header
            generator.DeclareLocal(typeof(bool));

            var srcFields = GetFieldsIncludingBaseClasses(sourceType, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            var destFields = GetFieldsIncludingBaseClasses(targetType, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (FieldInfo destField in destFields)
            {
                var srcField = srcFields.FirstOrDefault(x => x.Name == destField.Name);
                if (srcField != null && srcField.FieldType == destField.FieldType)
                {
                    generator.Emit(OpCodes.Ldarg_1);
                    generator.Emit(OpCodes.Ldarg_0);

                    generator.Emit(OpCodes.Ldfld, srcField);
                    generator.Emit(OpCodes.Stfld, destField);
                }
            }

            generator.Emit(OpCodes.Ret);

            // In case of State to StateDto it generates the following method:
            // public static void CopyObject(State source, StateDto target)
            // {
            //    target.Id = source.Id;
            //    target.Name = source.Name;
            //    target.ShortName = source.ShortName;
            // }

            var result = dymMethod.CreateDelegate(typeof(Action<In, Out>));
            return result;

        }

        private static List<FieldInfo> GetFieldsIncludingBaseClasses(Type type, BindingFlags bindingFlags)
        {
            var result = new List<FieldInfo>();

            // If this class doesn't have a base, don't waste any time
            if (type != typeof(object))
            {
                var fieldInfos = type.GetFields(bindingFlags);
                if (fieldInfos != null)
                {
                    foreach (var fi in fieldInfos)
                    {
                        if (!result.Any(x => x.Name == fi.Name))
                        {
                            result.Add(fi);
                        }
                    }
                }

                var baseFields = GetFieldsIncludingBaseClasses(type.BaseType!, bindingFlags);
                if (baseFields.Any())
                {
                    foreach (var fi in baseFields)
                    {
                        if (!result.Any(x => x.Name == fi.Name))
                        {
                            result.Add(fi);
                        }
                    }
                }
            }
            return result;
        }

        public static string GetFormatedNumericValueForInput(object value, int decimalPlaces)
        {
            if (value == null)
            {
                return string.Empty;
            }

            var valueType = value.GetType();

            if (valueType == typeof(int))
            {
                return ((int)value).ToString($"F{decimalPlaces}", CultureInfo.InvariantCulture);
            }
            if (valueType == typeof(int?))
            {
                return ((int?)value).Value.ToString($"F{decimalPlaces}", CultureInfo.InvariantCulture);
            }
            if (valueType == typeof(float))
            {
                return ((float)value).ToString($"F{decimalPlaces}", CultureInfo.InvariantCulture);
            }
            if (valueType == typeof(float?))
            {
                return ((float?)value).Value.ToString($"F{decimalPlaces}", CultureInfo.InvariantCulture);
            }
            if (valueType == typeof(double))
            {
                return ((double)value).ToString($"F{decimalPlaces}", CultureInfo.InvariantCulture);
            }
            if (valueType == typeof(double?))
            {
                return ((double?)value).Value.ToString($"F{decimalPlaces}", CultureInfo.InvariantCulture);
            }
            if (valueType == typeof(decimal))
            {
                return ((decimal)value).ToString($"F{decimalPlaces}", CultureInfo.InvariantCulture);
            }
            if (valueType == typeof(decimal?))
            {
                return ((decimal?)value).Value.ToString($"F{decimalPlaces}", CultureInfo.InvariantCulture);
            }
            return string.Empty;
        }

        public static decimal? GetRequestValueDecimal(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return new decimal?();
            }
            if (decimal.TryParse(input, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal value))
            {
                return value;
            }
            return new decimal?();
        }

        public static string GetRandomHtmlId()
        {
            var random = new Random();
            var val = random.Next(1000, 100000);
            return $"element{val:X2}";
        }

        public static DateTime? DecodeClientDateTime(string dateTime)
        {
            DateTime result;
            if (DateTime.TryParseExact(dateTime, "yyyy.MM.dd-HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                return result;
            }
            return new DateTime?();
        }

        public static bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new MailAddress(email);
                return string.Equals(addr.Address, trimmedEmail, StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        static bool IsMarkedAsNullable(PropertyInfo p)
        {
            return _nullabilityInfoContext.Create(p).WriteState is NullabilityState.Nullable;
        }


        public static List<string> ValidateManatoryFields<T>(T value)
        {
            var result = new List<string>();
            if (value == null)
            {
                return result;
            }

            var type = value.GetType();
            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);

            foreach (var property in properties)
            {
                var isNullable = IsMarkedAsNullable(property);
                if (!isNullable)
                {
                    var fv = property.GetValue(value);
                    if (fv == null)
                    {
                        result.Add(property.Name);
                    }
                }
            }

            return result;
        }

    }
}