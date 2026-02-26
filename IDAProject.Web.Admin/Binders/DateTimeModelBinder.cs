using Microsoft.AspNetCore.Mvc.ModelBinding;
using IDAProject.Web.Helpers;

namespace IDAProject.Web.Admin.Binders
{
    public class DateTimeModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            var value = valueProviderResult.FirstValue;

            if (string.IsNullOrEmpty(value))
            {
                return Task.CompletedTask;
            }

            // Remove group separators and trim the input value
            value = value.Replace(",", string.Empty).Trim();

            var dateTimeValue = DataHelpers.DecodeClientDateTime(value);

            if (dateTimeValue.HasValue)
            {
                bindingContext.Result = ModelBindingResult.Success(dateTimeValue.Value);
            }

            return Task.CompletedTask;
        }
    }
}