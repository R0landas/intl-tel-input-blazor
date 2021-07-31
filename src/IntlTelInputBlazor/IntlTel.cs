namespace IntlTelInputBlazor
{
    public class IntlTel
    {
        public string Number { get; set; }
        public bool IsValid { get; set; }
        public int ValidationError { get; set; }
        public IntlTelCountryData CountryData { get; set; }
        public string Extension { get; set; }
        public int NumberType { get; set; }
    }
}