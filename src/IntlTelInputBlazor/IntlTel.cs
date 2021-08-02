namespace IntlTelInputBlazor
{
    public class IntlTel
    {
        /// <summary>
        /// The telephone number entered by the user
        /// Number can be set before rendering the component to be displayed to the user
        /// </summary>
        /// <remarks>
        /// If you set the number, also set IsValid if the number is valid
        /// for validation to work properly the firs time</remarks>
        public string Number { get; set; }
        public bool IsValid { get; set; }
        public int ValidationError { get; set; }
        public IntlTelCountryData CountryData { get; set; }
        public string Extension { get; set; }
        public int NumberType { get; set; }
    }
}