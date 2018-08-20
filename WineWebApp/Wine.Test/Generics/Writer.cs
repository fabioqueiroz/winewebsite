using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Wine.Test.Generics
{
    public class Writer<T>
    {
        private T _value; 
        public Writer(T value)
        {
            _value = value;
        }

        public void Display()
        {
            Debug.WriteLine(_value);
        }

        public T GetCurrency<TD>(T currencyValue, TD country) where TD : CurrencyCountry
        {
            if (typeof (TD).Name == "Pound")
            {
                var p = country;

                Debug.WriteLine(p.CountryName);

                return currencyValue;                
            }

            return currencyValue;
        }
    }
}
