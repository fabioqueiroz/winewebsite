using System;
using System.Collections.Generic;
using System.Text;

namespace Wine.Data
{
    public class Wine
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int RegionId { get; set; }

        public bool Sparkling { get; set; }
      
        public decimal Price { get; set; }

        public string Description { get; set; }

        public virtual Region Region { get; set; }
    }
}
