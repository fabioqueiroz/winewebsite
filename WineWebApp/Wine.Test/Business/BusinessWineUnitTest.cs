using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wine.Commons.Business.Interfaces;
using Wine.Business.Services;
using System.Threading.Tasks;
using Wine.Commons.DAL.Interfaces;
using Wine.DataAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wine.Commons.Business.Models;

namespace Wine.Test.Business
{
    [TestClass]
    public class BusinessWineUnitTest // used to mock the concrete classes; integration is used to check in the DB
    {
        private IConfigurationRoot _configurationBuilder;
        private ServiceProvider _serviceProvider;

        private IWineService _wineService;
        private IWineRepository _wineRepository;

        [TestInitialize]
        public void SetUp()
        {
            _configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            _serviceProvider = new ServiceCollection()
                .AddScoped<IWineService, WineService>()
                .AddSingleton<IWineRepository>(new WineRepository(_configurationBuilder))
                .BuildServiceProvider();

            _wineService = _serviceProvider.GetService<IWineService>();
        }

        [TestMethod]
        public async Task GetWineById() 
        {
            // Arrange
            var newWineModel = new WineModel
            {
                Name = "Bordeaux",
                RegionID = 1,
                Sparkling = false,
                Price = 5.6M,
                Description = "Red"
            };

            var addWine = await _wineService.AddNewWine(newWineModel);

            // Act

            var output = await _wineService.GetWineById(newWineModel.ID);

            // Assert
            Assert.IsNotNull(output);
            Assert.IsTrue(output.ID > 0);
            Assert.AreEqual(output.ID, newWineModel.ID);
        }

        [TestMethod]
        public async Task GetAllWines() 
        {
            // Arrange
            var newWineModel = new WineModel
            {
                Name = "Marsala",
                RegionID = 3,
                Sparkling = false,
                Price = 12.3M,
                Description = "White"
            };

            var addWineModel = await _wineService.AddNewWine(newWineModel);

            // Act 
            var output = await _wineService.GetAllWines();

            // Assert
            Assert.IsNotNull(output);
            Assert.IsTrue(addWineModel.RegionID != 0);
            Assert.IsTrue(output.Count > 0);
        }

        [TestMethod]
        public async Task AddWine() 
        {
            // Arrange
            var  newWineModel = new WineModel
            {
                Name = "Valpolicella",
                RegionID = 5,
                Sparkling = false,
                Price = 7.9M,
                Description = "Red"
            };

            // Act
            var output = await _wineService.AddNewWine(newWineModel);

            // Assert
            Assert.IsNotNull(output);
            Assert.AreEqual(output.Name, newWineModel.Name);
            Assert.IsTrue(output.ID > 0);
        }

        [TestMethod]
        public async Task UpdateWineById() 
        {
            // Arrange
            var newWineModel = new WineModel
            {
                Name = "Prosecco",
                RegionID = 7,
                Sparkling = true,
                Price = 11.8M,
                Description = "White"
            };

            var addWineModel = await _wineService.AddNewWine(newWineModel);

            // Act
            var updWine = await _wineService.UpdateWineById(addWineModel);

            // Assert
            Assert.IsNotNull(updWine);
        }

        [TestMethod]
        public async Task DeleteWineById() // ** review **
        {
            // Arrange
            var newWineModel = new WineModel
            {
                Name = "Rioja",
                RegionID = 5,
                Sparkling = false,
                Price = 4.7M,
                Description = "Red"
            };

            var addWine = await _wineService.AddNewWine(newWineModel);

            // Act
            var delWine = await _wineService.DeleteWineById(addWine.ID);

            // Assert
            Assert.IsNotNull(delWine);


            ////test passed
            //var findWine = await _wineService.GetWineById(27);

            //var delWine = await _wineService.DeleteWineById(findWine.ID);

            //Assert.IsNotNull(delWine);
        }

    }
}
