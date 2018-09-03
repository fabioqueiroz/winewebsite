using System.ComponentModel.DataAnnotations.Schema;

namespace Wine.Commons.Business.Models
{
    public class WineModel
    {
        public int ID { get; set; }

        public string Name { get; set; }
       
        public bool Sparkling { get; set; }
      
        public decimal Price { get; set; }

        public string Description { get; set; }

        public int RegionId { get; set; }

        [ForeignKey("RegionId")]
        public virtual RegionModel Region { get; set; }
              
    }
}
