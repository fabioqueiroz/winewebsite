using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wine.Commons.Business.Interfaces;
using Wine.Business.Services;
using System.Threading.Tasks;
using Wine.Commons.DAL.Interfaces;
using Wine.DataAccess;
using Wine.Commons.Business.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Wine.Test.Business
{
    [TestClass]
    public class BusinessRegionUnitTest
    {
        private IConfigurationRoot _configurationBuilder;
        private ServiceProvider _serviceProvider;

        private IRegionService _regionService;
        private IWineRepository _wineRepository;

        [TestInitialize]
        public void SetUp()
        {
            _configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            _serviceProvider = new ServiceCollection()
                .AddScoped<IRegionService, RegionService>()
                .AddSingleton<IWineRepository>(new WineRepository(_configurationBuilder))
                .BuildServiceProvider();

            _regionService = _serviceProvider.GetService<IRegionService>();
        }

        [TestMethod]
        public async Task GetRegionByName() 
        {
            // Arrange
            var newRegion = new RegionModel { Name = "Veneto", CountryId = 4};

            var addRegion = await _regionService.AddNewRegion(newRegion);

            // Act
            var result = await _regionService.GetRegionByName(addRegion.Name);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Veneto", result.Name);
            Assert.AreEqual(newRegion.Name, result.Name);
            Assert.IsTrue(result.ID> 0); 
        }

        [TestMethod]
        public async Task GetAllRegions()
        {
            // Arrange
            var region = new RegionModel { Name = "Bordeaux", CountryId = 5 };

            var addRegion = await _regionService.AddNewRegion(region);

            // Act
            var output = await _regionService.GetAllRegions();

            // Assert
            Assert.IsNotNull(output);
            Assert.IsTrue(output.FirstOrDefault(x => x.Name == addRegion.Name) != null); 
            Assert.IsTrue(output.Count > 0);
        }

        [TestMethod]
        public async Task AddRegion()
        {
            // Arrange
            var regionModel = new RegionModel { Name = "Tuscany", CountryId = 6 };

            // Act
            var output = await _regionService.AddNewRegion(regionModel);

            // Assert
            Assert.IsNotNull(output);
            Assert.IsTrue(output.ID > 0); 
            Assert.AreEqual(output.Name, regionModel.Name);
        }

        [TestMethod]
        public async Task UpdateRegion() 
        {
            // Arrange

            var newRegion = new RegionModel { Name = "Bergerac", CountryId = 12 };

            var addRegion = await _regionService.AddNewRegion(newRegion);

            // Act
            var updRegion = await _regionService.UpdateRegion(addRegion);

            //Assert
            Assert.IsNotNull(updRegion);
            Assert.AreEqual(updRegion.Name, newRegion.Name);
            Assert.IsTrue(updRegion.ID > 0);
        }

        [TestMethod]
        public async Task DeleteRegionByName() 
        {
            // Arrange
            var region = new RegionModel { Name = "Algarve", CountryId = 6 };

            var addRegion = await _regionService.AddNewRegion(region);

            // Act
            var delRegion = await _regionService.DeleteRegionByName(addRegion.Name);

            // Assert
            Assert.IsNotNull(delRegion);
        }
    }
}
