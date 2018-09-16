using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wine.WebAPI.Models;

namespace Wine.WebAPI
{
    public class DummyDatabase
    {
        //List<WineAddViewModel> dummywineList { get; set; }
        //List<WineViewModel> dummydatabasewinelist { get; set; }
         public List<WineViewModel> dummywinelist;
         public List<WineViewModel> dummydatabaselist;

         public DummyDatabase()
         {
             dummywinelist = new List<WineViewModel>();
             dummydatabaselist = new List<WineViewModel>();
         }
    }
}
