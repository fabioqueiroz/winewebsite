using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wine.WebAPI.Models
{
    public class WineAddViewModel
    {
        [Required]
        public string Name { get; set; }

        [StringLength(5,ErrorMessage = "wrong length")]
        public string Description { get; set; }

        public string Country { get; set; }

        public string Region { get; set; }

        [Range(1,100,ErrorMessage = "enter the right amount")]
        public decimal Price { get; set; }

        public int Id { get; set; }
    }
}
