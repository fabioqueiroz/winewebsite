using System;
using System.Collections.Generic;
using System.Text;
using Wine.Commons.Business.Models;

namespace Wine.Commons.Business.Interfaces
{
    public interface IUserService
    {
        UserModel Register(UserModel model, string password);

           
    }
}
