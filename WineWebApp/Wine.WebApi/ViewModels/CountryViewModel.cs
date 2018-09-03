using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wine.WebAPI.ViewModels
{
    public class CountryViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public ICollection<RegionViewModel> Regions { get; set; }
    }
}
