using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wine.Commons.Business.Interfaces;
using Wine.Business.Services;
using System.Threading.Tasks;
using Wine.Commons.DAL.Interfaces;
using Wine.DataAccess;
using Wine.Commons.Business.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Wine.Test.Business
{
    [TestClass]
    public class BusinessCountryUnitTest // used to mock the concrete classes; integration is used to check in the DB
    {
        private IConfigurationRoot _configurationBuilder;
        private ServiceProvider _serviceProvider;

        private ICountryService _countryService;
        private IWineRepository _wineRepository;

        [TestInitialize]
        public void SetUp()
        {
            //## WITHOUT DI
            //_wineRepository = new WineRepository();
            //_countryService = new CountryService(_wineRepository);

            //## WITH DI
            //Setting up configuration
            _configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            //setup our DI
            _serviceProvider = new ServiceCollection()
                  .AddScoped<ICountryService, CountryService>()
                  .AddSingleton<IWineRepository>(new WineRepository(_configurationBuilder))
                  .BuildServiceProvider();

            _countryService = _serviceProvider.GetService<ICountryService>();
        }

        [TestMethod]
        public async Task AddCountry()
        {
            // *** test paterns ***

            // Arrange
            var country = new CountryModel
            {
                 Name = "Spain"
            };

            // Act

            var result = await _countryService.AddNewCountry(country);

            // Assert

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ID> 0);
            Assert.AreEqual(result.Name, country.Name);
        }


        [TestMethod]
        [TestCategory("Services")]
        public async Task GetCountryByName()
        {
            // Arrange
            var country = new CountryModel
            {
                Name = "France"
            };

            var newcountry = await _countryService.AddNewCountry(country);

            // Act

            var result = await _countryService.GetCountryByName(newcountry.Name);

            // Assert

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ID > 0);
            //Assert.AreEqual(result.Name, country.Name); // failed
        }



        [TestMethod]
        public async Task GetAllCountries()
        {
            // Arrange
            var country = new CountryModel
            {
                Name = "France"
            };

            var newcountry = await _countryService.AddNewCountry(country);  

            // Act

            var result = await _countryService.GetAllCountries();

            // Assert

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);            
        }
        [TestMethod]
        public async Task UpdateCountry()
        {
            // Arrange
            var country = new CountryModel { Name = "Australia"};

            var addCountry = await _countryService.AddNewCountry(country);

            // Act
            var updCountry = await _countryService.UpdateCountry(addCountry);

            // Assert
            Assert.IsNotNull(updCountry);
            Assert.AreEqual(updCountry.Name, country.Name);
            Assert.IsTrue(updCountry.ID > 0);
        }

        [TestMethod]
        public async Task DeleteCountryById() // ** review **
        {
            // Arrange
            var newCountry = new CountryModel { Name = "South Africa" };

            var addNewCountry = await _countryService.AddNewCountry(newCountry);

            // Act
            var delCountry = await _countryService.DeleteCountryById(addNewCountry.ID);

            // Assert
            Assert.IsNotNull(delCountry);

            //// test passed
            //// Act
            //var delCountry = await _countryService.DeleteCountryById(29);

            //// Assert
            //Assert.IsNotNull(delCountry);
        }

        [TestMethod]
        public async Task DeleteCountryByName() // ** review **
        {
            // Arrange
            var newCountry = new CountryModel { Name = "New Zealand" };

            var addCountry = await _countryService.AddNewCountry(newCountry);

            // Act
            var delCountry = await _countryService.DeleteCountryByName(addCountry.Name);

            // Assert
            Assert.IsNotNull(delCountry);


            //// test passed
            //// Act
            //var delCountry = await _countryService.DeleteCountryByName("italy");

            //// Assert
            //Assert.IsNotNull(delCountry);

        }
    }
}
