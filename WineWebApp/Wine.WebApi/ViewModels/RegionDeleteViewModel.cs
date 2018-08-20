using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wine.WebAPI.ViewModels
{
    public class RegionDeleteViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Country { get; set; }

        public string Region { get; set; }
    }
}
