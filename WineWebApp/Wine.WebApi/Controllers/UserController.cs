using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Wine.Commons.Business.Interfaces;
using Wine.Commons.Business.Models;
using Wine.WebApi.ViewModels;

namespace Wine.WebApi.Controllers
{
    public class UserController : Controller
    {
        private IUserService _userService;
        private IMapper _mapper;
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register([FromBody]UserViewModel newUser)
        {

            try
            {
                _userService.Register(_mapper.Map<UserModel>(newUser), newUser.Password);

                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }

        }
    }
}