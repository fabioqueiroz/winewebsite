using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Wine.Business.Services;
using Wine.Commons.Business.Interfaces;
using Wine.Commons.Business.Models;
using Wine.Commons.CrossCutting;
using Wine.Commons.DAL.Interfaces;
using Wine.Data;
using Wine.DataAccess;
using WineWebApp.Controllers;

namespace Wine.Test.Reflection
{
    [TestClass]
    public class MappingTester
    {
        private Mapper _map;

        private IConfigurationRoot _configurationBuilder;
        private ServiceProvider _serviceProvider;

        private ICountryService _countryService;
        private IWineRepository _wineRepository;

        [TestInitialize]
        public void SetUp()
        {

            _configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            _serviceProvider = new ServiceCollection()
                .AddScoped<ICountryService, CountryService>()
                .AddSingleton<ICountryRepository>(new CountryRepository(_configurationBuilder))
                .BuildServiceProvider();

            _countryService = _serviceProvider.GetService<ICountryService>();
        }

        [TestMethod]
        public void TestMapper()
        {
            var source = new Country { ID = 666, Name = "Australia"};
            var destination = new CountryModel { ID = 777, Name = "Italy" };

            var test = Mapper.UpdateMapper(source, destination);

            Assert.AreEqual(destination.Name, source.Name);
        }

        [TestMethod]
        public void TestMapperGenerics()
        {
            var source = new Country { ID = 666, Name = "Australia" };
            var destination = new CountryModel { ID = 777, Name = "Italy" };

            var test = Mapper.UpdateGenerics(source, destination);

            Assert.AreEqual(destination.Name, source.Name);
        }

        [TestMethod]
        public void TestMapperParams()
        {
            var source = new Country { ID = 666, Name = "Australia" };
            var destination = new CountryModel { ID = 777, Name = "Italy" };

            var test = Mapper.UpdateParamsMapper(source, destination, "ID");

            Assert.AreEqual(((CountryModel)test).ID, source.ID);
        }
    }
}
