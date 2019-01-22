using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wine.WebAPI.Models;
using Wine.WebAPI.ViewModels;
using System.Net.Http;
using System.Security;
using System.Diagnostics;
using Wine.DataAccess;
using Wine.Commons.Exceptions;

namespace Wine.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/wine")]
    public class WineController : Controller
    {
        //private DummyDatabase _db;
        //public WineController()
        //{
        //    _db = new DummyDatabase(); // list initialisation
        //}
        // Post action:  _db.dummywinelist.Add(model);

        private Context _context;
        public WineController(Context context)
        {
            _context = context;
        }

        //[HttpGet] // attribute to the routing table
        //public JsonResult GetWines()
        //{
        //    //anonimous object
        //    return new JsonResult(new List<object>
        //    {
        //        new {id = 1, name = "rioja", description = "red"},
        //        new {id = 2, name = "verdejo", description = "white"},
        //        new {id = 3, name = "barolo", description = "red"},
        //        new {id = 4, name = "falanghina", description = "white"}
        //    });
        //}

        [HttpGet("{id}")]
        public IActionResult GetWinebyId(int? id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest();
                }

                var wineModel = _context.Wines.Where(x => x.ID == id).FirstOrDefault();

                var wineResponse = new WineViewModel()
                {
                    ID = wineModel.ID,
                    Name = wineModel.Name,
                    Description = wineModel.Description,
                    Price = wineModel.Price,
                    Sparkling = wineModel.Sparkling,
                    RegionId = wineModel.RegionId
                };

                if (wineResponse == null)
                {
                    throw new ItemNotFoundExceptions("The id was not found");
                }

                return Ok(wineResponse);
            }

            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return BadRequest();
            }
        }

        //[HttpGet("{id:int}/{description}")]
        //public IActionResult GetWinebyIdandDescription(int id, string description)
        //{
        //    //way to fill in a list with objects in C#
        //    List<WineViewModel> winelist = new List<WineViewModel>()
        //    {
        //        new WineViewModel
        //        {
        //             Id = 1, Name = "rioja", Description = "red"
        //        },
        //        new WineViewModel
        //        {
        //            Id = 2, Name = "verdejo", Description = "white"
        //        },
        //         new WineViewModel
        //        {
        //            Id = 3, Name = "barolo", Description = "red"
        //        },
        //        new WineViewModel
        //        {
        //            Id = 4, Name = "falanghina", Description = "white"
        //        }
        //    };

        //    return Ok(winelist.Where(x => x.Id == id && x.Description == description).FirstOrDefault());
        //}

 
        [HttpPost]
        public IActionResult AddWine([FromBody]WineViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                if (model == null)
                {
                    return BadRequest();
                }

                var addNewWine = new Wine.Data.Wine()
                {
                    // Id not added because the DB will generate it automatically             
                    Name = model.Name,
                    Description = model.Description,                   
                    Price = model.Price,
                    Sparkling = model.Sparkling,
                    RegionId = model.RegionId
                };

                if (addNewWine != null)
                {
                    _context.Add(addNewWine);

                    _context.SaveChanges();
                }

                var response = new WineViewModel()
                {
                    ID = addNewWine.ID,
                    Name = addNewWine.Name,
                    Description = addNewWine.Description,
                    Price = addNewWine.Price,
                    Sparkling = addNewWine.Sparkling,
                    RegionId = addNewWine.RegionId
                };

                return Ok(response);
            }
            
            catch(Exception ex)
            {
                Trace.TraceError(ex.Message);
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult UpdateWine([FromBody]WineViewModel updModel)
        {
            try
            {
                if (!ModelState.IsValid || updModel == null)
                {
                    return BadRequest();
                }

                // mapping 1
                var wine = _context.Wines.Where(x => x.ID == updModel.ID).FirstOrDefault();

                if (wine != null)
                {
                    wine.Name = updModel.Name;
                    wine.Description = updModel.Description;
                    wine.Price = updModel.Price;
                    wine.Sparkling = updModel.Sparkling;
                    wine.RegionId = updModel.RegionId;

                    _context.Update(wine);
                    _context.SaveChanges();
                }

                var updResponse = new WineViewModel()
                {
                    ID = wine.ID,
                    Name = wine.Name,
                    Description = wine.Description,
                    Price = wine.Price,
                    Sparkling = wine.Sparkling,
                    RegionId = wine.RegionId

                };

                return Ok(updResponse);
            }
            
            catch(Exception ex)
            {
                Trace.TraceError(ex.Message);
                return BadRequest();
            }
        }
        
        [HttpDelete("{id:int}")]
        public IActionResult DeleteWine(int? id) 
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                CheckWebsiteId(id);

                var wine = _context.Wines.Where(x => x.ID == id).FirstOrDefault();  
                
                if (wine != null)
                {
                    _context.Remove(wine);
                    _context.SaveChanges();

                    Trace.TraceInformation($"the wine {id} {wine.Name} was deleted");
                }
                
                return Ok();
            }
            
            catch(ItemNotFoundExceptions ex)
            {
                Trace.TraceError(ex.Message);
                return StatusCode(429);
            }

            catch(HttpRequestException ex) // priority 3
            {
                Trace.TraceError(ex.Message);
                return BadRequest();
            }

            catch (SecurityException ex) // priority 2
            {
                Trace.TraceError(ex.Message);
                return Forbid();
            }

            catch (Exception ex) // priority 1
            {
                Trace.TraceError(ex.Message); 
                return BadRequest();
            }
           

        }

        private void CheckWebsiteId(int? id)
        {
            if (id < 1)
            {
                throw new ItemNotFoundExceptions("Ilegal value");
            }
        }
    }
    
}  
