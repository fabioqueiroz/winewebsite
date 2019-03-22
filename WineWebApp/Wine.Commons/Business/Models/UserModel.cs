using System;
using System.Collections.Generic;
using System.Text;

namespace Wine.Commons.Business.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public byte[] Hash { get; set; }

        public byte[] Salt { get; set; }
    }
}
