using System.ComponentModel.DataAnnotations.Schema;

namespace WineWebApp.ViewModels
{
    public class WineViewModel
    {
        //*** these are equal to the below get/set accessors ***

        /* *** 1st possibility: ***
        private int _id;
        public int Id()
        {
            return _id;
        }

        public void Id(int id)
        {
            _id = id;
        }*/

        /* *** 2nd possibility: ***
        private int _id;
        public int Id
        {
            get => _id;
            set => _id = value;
        }*/

        // *** 3rd possibility: ***
        public int ID { get; set; }

        public string Name { get; set; }
        
        public bool Sparkling { get; set; }
       
        public decimal Price { get; set; }

        public string Description { get; set; }

        public int RegionId { get; set; }

        [ForeignKey("RegionId")]
        public virtual RegionViewModel Region { get; set; }

    }
}