using IntlTelInputBlazor;
using IntlTelInputBlazor.Validation;

namespace SimpleNumberInput.Models
{
    public class NumberModel
    {
        [IntlTelephone(ErrorMessage = "Tel. 1 incorrect format")]
        public IntlTel IntTelNumber { get; set; }
        
        [IntlTelephone(ErrorMessage = "Tel. 2 incorrect format")]
        public IntlTel IntTelNumber2 { get; set; }
    }
}