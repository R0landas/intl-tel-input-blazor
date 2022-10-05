using System;
using System.Collections.Generic;

namespace IntlTelInputBlazor
{
    public class IntlTel : IEquatable<IntlTel>
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

        public override bool Equals(object obj)
        {
            return Equals(obj as IntlTel);
        }

        public bool Equals(IntlTel other)
        {
            return other is not null &&
                   Number == other.Number;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Number);
        }

        public static bool operator ==(IntlTel left, IntlTel right)
        {
            return EqualityComparer<IntlTel>.Default.Equals(left, right);
        }

        public static bool operator !=(IntlTel left, IntlTel right)
        {
            return !(left == right);
        }
    }
}