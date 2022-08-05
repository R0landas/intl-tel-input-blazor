using IntlTelInputBlazor;
using IntlTelInputBlazor.Validation;

namespace SimpleNumberInput.Models
{
    public class NumberModel
    {
        private IntlTel intTelNumber2;
        private IntlTel intTelNumber;

        public string Number { get; set; }

        [IntlTelephone(ErrorMessage = "Tel. 1 incorrect format")]
        public IntlTel IntTelNumber
        {
            get => string.IsNullOrWhiteSpace(Number) ? intTelNumber : new() { Number = Number, IsValid = true };
            set
            {
                intTelNumber = value;

                if (value?.IsValid == true)
                    Number = value.Number;
                else
                {
                    Number = null;

                    if (value?.Number == string.Empty)
                        intTelNumber = null;
                }
            }
        }

        public string Number2 { get; set; }

        [IntlTelephone(ErrorMessage = "Tel. 2 incorrect format")]
        public IntlTel IntTelNumber2
        {
            get => string.IsNullOrWhiteSpace(Number2) ? intTelNumber2 : new() { Number = Number2, IsValid = true };
            set
            {
                intTelNumber2 = value;

                if (value?.IsValid == true)
                    Number2 = value.Number;
                else
                {
                    Number2 = null;

                    if (value?.Number == string.Empty)
                        intTelNumber2 = null;
                }
            }
        }
    }
}