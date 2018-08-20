using System;
using System.Collections.Generic;
using System.Text;

namespace Wine.Test.Generics
{
    public abstract class CurrencyCountry  // the abstract modifier in a class declaration is used to indicate that a class is intended only to be a base class of other classes
    {
        public string CountryName { get; set; }
    }

    public class Pound : CurrencyCountry
    {
        public int ExchangeRate { get; set; }

        public int Value { get; set; }
    }

    public class Dollar : CurrencyCountry
    {
        public int ExchangeRate { get; set; }

        public int Value { get; set; }
    }
}


