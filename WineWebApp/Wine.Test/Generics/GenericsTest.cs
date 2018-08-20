using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Wine.Test.Generics;

namespace Wine.Test
{
   [TestClass]
   public class GenericsTest
    {
        [TestMethod]
        public void GenericTest()
        {
            var writer = new Writer<string>("wine");

            writer.Display();

        }

        [TestMethod]
        public void GetCurrency()
        {
            var writer = new Writer<int>(10);

            var result = writer.GetCurrency<Pound>(20, new Pound { CountryName = "UK", ExchangeRate = 2, Value = 30 });

            Debug.WriteLine(result);
        }
    }
}
