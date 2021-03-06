﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Wine.WebAPI.Models;

namespace Wine.WebAPI.ViewModels
{
    public class RegionViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public virtual CountryViewModel Country { get; set; }

        public virtual ICollection<WineViewModel> Wines { get; set; }
    }
}
