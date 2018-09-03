using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Wine.Data
{
    public class Wine
    {
        public int ID { get; set; }

        public string Name { get; set; }
       
        public bool Sparkling { get; set; }
      
        public decimal Price { get; set; }

        public string Description { get; set; }

        public int RegionId { get; set; }

        [ForeignKey("RegionId")]
        public virtual Region Region { get; set; }
    }
}
