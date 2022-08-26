using FluentValidation;
using FluentValidation.Validators;
using System;

namespace Boyner.Product.Application.SeedWork.Validations
{
    public class GuidValidator<T, TProperty> : PropertyValidator<T, TProperty>
    {
        public override string Name => "GuidValidator";

        public override bool IsValid(ValidationContext<T> context, TProperty value)
        {
            if (!Guid.TryParse(value?.ToString(), out Guid guidValue))
                return false;

            if (Guid.TryParseExact(guidValue.ToString("B"), "B", out Guid guid))
                return true;

            return false;
        }

        protected override string GetDefaultMessageTemplate(string errorCode)
          => "{PropertyName} must contain UUID {PropertyValue}";
    }
}
