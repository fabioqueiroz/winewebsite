namespace Wine.Commons.Business.Models
{
    public class WineModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int RegionID { get; set; }

        public bool Sparkling { get; set; }
      
        public decimal Price { get; set; }

        public string Description { get; set; }

        public virtual RegionModel Region { get; set; }
      
        //public string Country { get; set; }

        
    }
}
