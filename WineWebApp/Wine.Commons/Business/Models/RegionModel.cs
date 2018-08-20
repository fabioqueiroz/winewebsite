using System.Collections.Generic;

namespace Wine.Commons.Business.Models
{
    public class RegionModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int CountryID { get; set; }

        public virtual CountryModel Country { get; set; }

        public virtual ICollection<WineModel> Wines { get; set; }
    }
}