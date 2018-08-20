using System.Collections.Generic;

namespace Wine.Commons.Business.Models
{
    public class CountryModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public virtual ICollection<RegionModel> Regions { get; set; }
    }
}