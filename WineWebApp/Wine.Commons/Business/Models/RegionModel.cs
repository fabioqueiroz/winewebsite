using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wine.Commons.Business.Models
{
    public class RegionModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public virtual CountryModel Country { get; set; }

        public virtual ICollection<WineModel> Wines { get; set; }
    }
}