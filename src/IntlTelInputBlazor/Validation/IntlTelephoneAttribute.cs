using System;
using System.ComponentModel.DataAnnotations;

namespace IntlTelInputBlazor.Validation
{
    public class IntlTelephoneAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is null)
            {
                return new ValidationResult(ErrorMessage, new []{validationContext.MemberName});
            }
            
            if (value is not IntlTel intlTel)
            {
                throw new InvalidOperationException($"{nameof(IntlTelephoneAttribute)} can only validate {nameof(intlTel)}");
            }
            return intlTel.IsValid ? ValidationResult.Success : new ValidationResult(ErrorMessage, new []{validationContext.MemberName});
        }
    }
}