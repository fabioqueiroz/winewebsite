using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Wine.Business.Services;
using Wine.Commons.Business.Interfaces;
using Wine.Commons.CrossCutting;
using Wine.Commons.DAL.Interfaces;
using Wine.Data;
using Wine.WebApi.Controllers;

namespace Wine.Test.Business
{
    [TestClass]
    public class BusinessUserUnitTest
    {
        private IUserService _userService;
        private IWineRepository _wineRepository;
        private IMapper _mapper;
        private UserController _userController;

        [TestInitialize]
        public void SetUp()
        {
            // Integration testing
            //_userService = new UserService(_wineRepository);
            //_userController = new UserController(_userService, _mapper);

           
        }

       [TestMethod]
       public async Task TestOne()
        {
            // Unit testing
            var repoMock = new Mock<IWineRepository>();

            repoMock.Setup(x => x.GetSingleAsync<User>(It.IsAny<Expression<Func<User, bool>>>())).Returns(Task.FromResult(new User { FirstName = "test name"}));

            _userService = new UserService(repoMock.Object);
           
            var user = await _userService.GetUserByName("mock name");

            Assert.AreEqual(user.FirstName, "test name");
        }
        
    }
}
