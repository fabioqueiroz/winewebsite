using System;
using System.Collections.Generic;
using System.Text;

namespace Wine.Data
{
    public class Region
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int CountryID { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<Wine> Wines { get; set; }
    }
}
