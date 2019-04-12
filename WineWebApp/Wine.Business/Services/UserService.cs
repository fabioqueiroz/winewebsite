using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wine.Commons.Business.Interfaces;
using Wine.Commons.Business.Models;
using Wine.Commons.CrossCutting;
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

            byte[] hashPasswordInDb = Encryptor.Hash(password, model.Salt);

            var newUser = _repository.Add<User>(new User
            {
                 FirstName = model.FirstName,
                 LastName = model.LastName,
                 UserName = model.UserName,
                 Hash = hashPasswordInDb
                 
            });

            _repository.Commit();

            return model;
        }

        public Task<UserModel> GetUserByName(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> UpdateUserInfo(UserModel userInfo)
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> UpdateUserPassword(UserUpdateModel userUpdateModel, string oldPassword, string newPassword, string resetPassword)
        {
            throw new NotImplementedException();
        }
        public Task<UserModel> DeleteUser(string name)
        {
            throw new NotImplementedException();
        }
    }
}
