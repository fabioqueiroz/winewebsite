using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wine.Commons.Business.Models;

namespace Wine.Commons.Business.Interfaces
{
    public interface IUserService
    {
        UserModel Register(UserModel model, string password);

        Task<UserModel> GetUserByName(string userName);

        Task<UserModel> UpdateUserInfo(UserModel userInfo);

        Task<UserUpdateModel> UpdateUserPassword(UserUpdateModel userUpdateModel, string oldPassword, string newPassword, string resetPassword);

        Task<bool> DeleteUser(string name);
    }
}
