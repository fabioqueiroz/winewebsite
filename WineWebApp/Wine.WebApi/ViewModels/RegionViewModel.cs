using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wine.WebAPI.Models;

namespace Wine.WebAPI.ViewModels
{
    public class RegionViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public ICollection<WineViewModel> Wines { get; set; }
    }
}
