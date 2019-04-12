using System;
using System.Collections.Generic;
using System.Text;

namespace Wine.Commons.Business.Models
{
    public class UserUpdateModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public string RepeatPassword { get; set; }
    }
}
