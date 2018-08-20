using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Linq;

namespace Wine.Test.Delegates
{
    [TestClass]
    public class FlightTracker
    {
        private Airport _delegate;

        [TestInitialize]
        public void SetUp()
        {
            _delegate = new Airport();
        }

        [TestMethod]
        public void SearchFlight()
        {
            Func<string, string, string> GetDirectFlight = _delegate.DirectConnection;
            var result = GetDirectFlight("HHH", "KKK");

            Func<string, string, string, string> GetConnectionFlight = _delegate.AlternativeConnection;
            var newResult = GetConnectionFlight("EEE", "BBB", "TTT");

            Debug.WriteLine(result);
            Debug.WriteLine(newResult);
        }

        [TestMethod]
        public void CheckPrice()
        {
            Func<int, int, int> FindDirectPrice = _delegate.DirectPrice;
            int result = FindDirectPrice(10, 20);

            Func<int, int, int, int> FindAltPrice = _delegate.ConnectionPrice;
            int newResult = FindAltPrice(10, 20, 40);

            Assert.AreEqual(30, result);
            Debug.WriteLine(result);
            Debug.WriteLine(newResult);
        }

        public void CalculateRoute<T, P>(T route, P price) where T : Airport
        {
            var routeResult = _delegate.DirectConnection("HHH", "KKK");
            var priceResult = _delegate.DirectPrice(10, 20);

            Debug.WriteLine(routeResult, priceResult);
        }

    }

    public class Airport
    {
        //private List<Airport> _airportList;

        public int ID { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        //public List<Airport> AirportList { get => _airportList; set => _airportList = value; }

        //private int ID;
        //private string Name;
        //private int Price;

        //public Airport(int id, string name, int price)
        //{
        //    ID = id;
        //    Name = name;
        //    Price = price;
        //}

        public string GetName(string name)
        {
            Name = name;
            return name;
        }

        public int GetPrice(int price)
        {
            Price = price;
            return price;
        }

        public string DirectConnection(string departureName, string arrivalName)
        {
            departureName = "XXX";
            arrivalName = "YYY";

            return ($"{departureName} - {arrivalName}");
        }
        public int DirectPrice(int x, int y) => x + y;


        public string AlternativeConnection(string departureName, string connectionName, string arrivalName)
        {
            return ($"{departureName} - {connectionName} - {arrivalName}");
        }
        public int ConnectionPrice(int x, int y, int z) => x + y + z;
    }

    public class PopulateList
    {
        private List<Airport> _listvalues;

        public PopulateList()
        {
            _listvalues = new List<Airport>();
            //_listvalues.Add(new Airport(1, "GGG", 30));
            //_listvalues.Add(new Airport(2, "FFF", 40));
            Airport One = new Airport() { ID = 1, Name = "WWW", Price = 30 };
            Airport Two = new Airport() { ID = 2, Name = "ZZZ", Price = 40 };           
        }       
    }
}

