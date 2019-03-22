using System;
using System.Collections.Generic;
using System.Text;
using Wine.Commons.Business.Interfaces;
using Wine.Commons.Business.Models;
using Wine.Commons.DAL.Interfaces;
using Wine.Commons.Exceptions;
using Wine.Data;

namespace Wine.Business.Services
{
    public class UserService : IUserService
    {
        private IWineRepository _repository;
        public UserService(IWineRepository repository)
        {
            _repository = repository;
        }

        public UserModel Register(UserModel model, string password)
        {

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new UserExceptions("Invalid password");
            }

            var newUser = _repository.Add<User>(new User
            {
                 FirstName = model.FirstName,
                 LastName = model.LastName,
                 UserName = model.UserName,
                 
            });

            return model;
        }
    }
}
