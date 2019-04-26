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

        public async Task<UserModel> GetUserByName(string userName)
        {
            var user = await _repository.GetSingleAsync<User>(x => x.UserName.Equals(userName));

            var userModel = new UserModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName
            };

            return userModel;
        }

        public async Task<UserModel> UpdateUserInfo(UserModel userInfo)
        {
            var user = await _repository.GetSingleAsync<User>(x => x.UserName.Equals(userInfo.UserName));

            user.FirstName = userInfo.FirstName;
            user.LastName = userInfo.LastName;
            user.UserName = userInfo.UserName;

            _repository.Update<User>(user);

            await _repository.CommitAsync();

            return userInfo;
        }

        public async Task<UserUpdateModel> UpdateUserPassword(UserUpdateModel userUpdateModel, string oldPassword, string newPassword, string resetPassword)
        {
            var userInDb = await _repository.GetSingleAsync<User>(x => x.UserName.Equals(userUpdateModel.UserName));

            if (string.IsNullOrWhiteSpace(oldPassword) || string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(resetPassword))
            {
                throw new UserExceptions("Invalid password");
            }

            byte[] hashPasswordInDb = Encryptor.Hash(oldPassword, userInDb.Salt);

            if (!Encryptor.PasswordChecker(oldPassword, userInDb.Salt, hashPasswordInDb))
            {
                throw new UserExceptions("Invalid password");
            }

            if (!oldPassword.Equals(newPassword) && !oldPassword.Equals(resetPassword) && newPassword.Equals(resetPassword))
            {
                userInDb.Hash = Encryptor.Hash(newPassword, userInDb.Hash);
            }

            _repository.Update<User>(userInDb);

            await _repository.CommitAsync();

            return userUpdateModel;
        }

        public async Task<bool> DeleteUser(string userName)
        {
            var user = await _repository.GetSingleAsync<User>(x => x.UserName.Equals(userName));

            _repository.Delete<User>(user);

            await _repository.CommitAsync();

            return true;
        }
    }
}
