using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wine.WebApi.ViewModels
{
    public class UserUpdateViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public string RepeatPassword { get; set; }
    }
}
