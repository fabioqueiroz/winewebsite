using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wine.WebAPI.Models
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
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Country { get; set; }

        public string Region { get; set; }

        public decimal Price { get; set; }

    }
}


