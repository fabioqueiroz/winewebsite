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
using Wine.Commons.Business.Models;
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
       public async Task TestGetUser()
        {
            // Unit testing
            // Arrange
            var repoMock = new Mock<IWineRepository>();

            // Act 
            repoMock.Setup(x => x.GetSingleAsync<User>(It.IsAny<Expression<Func<User, bool>>>())).Returns(Task.FromResult(new User { FirstName = "test name"}));

             _userService = new UserService(repoMock.Object);
           
            var user = await _userService.GetUserByName("mock name");

            // Assert
            Assert.AreEqual(user.FirstName, "test name");
        }

        // TO FIX
        [TestMethod]
        public async Task TestRegistration()
        {
            var repoMock = new Mock<IWineRepository>();
     

            var user = new User { FirstName = "fname", LastName = "lname", UserName = "username",
                 Salt = new byte[]{ 0x00C9, 0x00C8, 0x00C1 }, Hash = new byte[] { 0x00C6, 0x00C9, 0x00C2 }};

            repoMock.Setup(x => x.Add<User>(user)).Returns(new User {FirstName = "fname", LastName = "lname", UserName = "username",
                Id = 2, Salt = new byte[] { 0x00C9, 0x00C8, 0x00C1 }, Hash = new byte[] { 0x00C6, 0x00C9, 0x00C2 }});

            var mockMapper = new Mock<IMapper>();

            mockMapper.Setup(x => x.Map <User, UserModel>(It.IsAny<User>())).Returns(new UserModel
            {
                FirstName = "fname",
                LastName = "lname",
                UserName = "username",
                Id = 2,
                Salt = new byte[] { 0x00C9, 0x00C8, 0x00C1 },
                Hash = new byte[] { 0x00C6, 0x00C9, 0x00C2 }
            });


            _userService = new UserService(repoMock.Object);

            var userModel = _userService.Register(new UserModel
            {
                FirstName = "fname",
                LastName = "lname",
                UserName = "username",
                Salt = new byte[] { 0x00C9, 0x00C8, 0x00C1 },
                Hash = new byte[] { 0x00C6, 0x00C9, 0x00C2 }
            }, "password");

           //Assert.AreEqual(userModel.UserName, "username");
            Assert.AreEqual(userModel.Id, 2);

        }
        
        [TestMethod]
        public async Task TestUpdateInfo()
        {
            var repoMock = new Mock<IWineRepository>();

            repoMock.Setup(x => x.GetSingleAsync<User>(It.IsAny <Expression<Func<User, bool>>>()))
                .Returns(Task.FromResult<User>(new User { UserName = " mock username"}));

            repoMock.Setup(x => x.Update<User>(It.IsAny<User>())).Returns(new User { UserName = "username" });

            _userService = new UserService(repoMock.Object);

            var userModel = new UserModel
            {
                FirstName = "fname",
                LastName = "lname",
                UserName = "username",
                Salt = new byte[] { 0x00C9, 0x00C8, 0x00C1 },
                Hash = new byte[] { 0x00C6, 0x00C9, 0x00C2 }
            };

            var newUser = await _userService.UpdateUserInfo(userModel);      

            Assert.AreEqual(newUser.UserName, userModel.UserName);
        }

        // missing test UpdateUserPassword

        [TestMethod]
        public async Task TestDelete()
        {
            var repoMock = new Mock<IWineRepository>();
            var user = new User { UserName = "username" };

            repoMock.Setup(x => x.GetSingleAsync<User>(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(Task.FromResult<User>(user));

            repoMock.Setup(x => x.Delete<User>(It.IsAny<User>())).Returns<User>(null);

            _userService = new UserService(repoMock.Object);            

            var delUser = await _userService.DeleteUser(user.UserName);

            Assert.IsTrue(delUser);
        }
    }
}
