using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WineWebApp.ViewModels
{
    public class RegionViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int CountryID { get; set; }

        public virtual CountryViewModel Country { get; set; }

        public ICollection<WineViewModel> Wines { get; set; }
    }
}
