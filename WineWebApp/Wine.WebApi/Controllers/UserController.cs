using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wine.Commons.Business.Interfaces;
using Wine.Commons.Business.Models;
using Wine.Commons.Exceptions;
using Wine.WebApi.ViewModels;

namespace Wine.WebApi.Controllers
{
    [Route("api/user")]
    [Authorize]
    public class UserController : Controller
    {
        private IUserService _userService;
        private IMapper _mapper;
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet("{userName}")]
        public async Task<IActionResult> GetUser(string userName)
        {
            var userInDb = await _userService.GetUserByName(userName);

            //var viewModel = new UserViewModel
            //{
            //    FirstName = userInDb.FirstName,
            //    LastName = userInDb.LastName,
            //    UserName = userInDb.UserName
            //};

            var viewModel = _mapper.Map<UserModel,UserViewModel>(userInDb);

            return View(viewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register([FromBody]UserViewModel newUser)
        {

            try
            {
                _userService.Register(_mapper.Map<UserModel>(newUser), newUser.OldPassword);

                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }

        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody]UserViewModel updUser)
        {
            var userInDb = await _userService.GetUserByName(updUser.UserName);

            await _userService.UpdateUserInfo(userInDb);

            return View(updUser);
        }

        [HttpPut("password")]
        public async Task<IActionResult> UpdatePassword([FromBody]UserViewModel user)
        {
            var userInDb = await _userService.GetUserByName(user.UserName);

            if (userInDb.FirstName.Equals(user.FirstName) && userInDb.LastName.Equals(user.LastName))
            {
                 await _userService.UpdateUserPassword(_mapper.Map<UserViewModel, UserUpdateModel>(user), user.OldPassword, user.NewPassword, user.ResetPassword);

                 return Ok();
            }

            else
            {
                return BadRequest();
            }

        }

        [HttpDelete("{userName:string}")]
        public async Task<IActionResult> DeleteUser(string userName)
        {
            try
            {
                if (userName != null)
                {
                    await _userService.DeleteUser(userName);
                }

                return Ok();
            }
            catch (UserExceptions)
            {
                throw new UserExceptions("Username not found");
            }
        }
    }
}