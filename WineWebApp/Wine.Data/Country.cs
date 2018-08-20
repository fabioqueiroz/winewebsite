using System;
using System.Collections.Generic;
using System.Text;

namespace Wine.Data
{
    public class Country
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Region> Regions { get; set; }
    }
}
