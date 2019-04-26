using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wine.WebApi.ViewModels
{
    public class UserViewModel
    {
     
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        public string ResetPassword { get; set; }

    }
}
