using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wine.WebAPI.ViewModels;
using Wine.WebAPI.Models;
using System.Net.Http;
using System.Diagnostics;
using System.Security;
using Microsoft.EntityFrameworkCore;
using Wine.DataAccess;

namespace Wine.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/region")]
    public class RegionController : Controller
    {
        private Context _context;
        public RegionController(Context context)
        {
            _context = context;
        }

        //[HttpGet]
        //public JsonResult GetRegions()
        //{
        //    return new JsonResult(new List<object>()
        //    {
        //        new {id = 1, name = "rioja", region = "rioja"},
        //        new {id = 2, name = "verdejo", region = "rueda"},
        //        new {id = 3, name = "barolo", region = "piemonte"},
        //        new {id =4, name = "falanghina", region = "campania"}
        //    });
        //}

        [HttpGet("{region}")]
        public IActionResult GetRegion(string region)
        {
            try
            {
                if (region == null)
                {
                    return BadRequest();
                }

                var regionModel = _context.Regions.Where(x => x.Name == region).FirstOrDefault();

                var regionResponse = new RegionViewModel()
                {
                    ID = regionModel.ID,
                    Name = regionModel.Name,
                    CountryId = regionModel.CountryId,

                };

                return Ok(regionResponse);
            }

            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return BadRequest();
            }
        }

        [HttpGet("wines/{region}")] //RESTful protocol; wines is NOT a parameter here
        public IActionResult GetWineRegion(string region)
        {
            try
            {
                if (region == null)
                {
                    return BadRequest();
                }

                var regionSelectModel = _context.Regions.Include(x => x.Wines).FirstOrDefault(x => x.Name == region);

                var regionResponse = new RegionViewModel()
                {
                    ID = regionSelectModel.ID,
                    Name = regionSelectModel.Name,
                    CountryId = regionSelectModel.CountryId,
                    Wines = regionSelectModel.Wines != null ? regionSelectModel.Wines.Select(x => new WineViewModel
                    {
                        ID = x.ID,
                        Name = x.Name,
                        Description = x.Description,
                        Price = x.Price, 
                        Sparkling = x.Sparkling,
                        RegionId = x.RegionId
                        
                    }).ToList() : null
                };

                // **** option 2 ******

                //var regionResponse2 = new RegionViewModel();
                //if (regionSelectModel.Wines != null)
                //{
                //    foreach (var item in regionSelectModel.Wines)
                //    {
                //        regionResponse2.Wines.Add(new WineViewModel
                //        {
                //            Name = item.Name,
                //            Description = item.Description
                //        });
                //    }
                //}
                //regionResponse2.Id = regionSelectModel.ID;
                //regionResponse2.Name = regionSelectModel.Name;

                return Ok(regionResponse);
            }


            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult AddRegion([FromBody]RegionViewModel regionModel)
        {
            try
            {
                if (!ModelState.IsValid || regionModel == null)
                {
                    return BadRequest();
                }

                var addRegion = new Wine.Data.Region()
                {
                    Name = regionModel.Name,
                    CountryId = regionModel.CountryId
                };

                if (addRegion != null)
                {
                    _context.Add(addRegion);
                    _context.SaveChanges();
                }

                var regionResponse = new RegionViewModel()
                {
                    ID = addRegion.ID,
                    Name = addRegion.Name,
                    CountryId = addRegion.CountryId
                };

                return Ok(regionResponse);
            }
            
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult UpdateRegion([FromBody] RegionViewModel updRegionModel)
        {
            try
            {
                if (!ModelState.IsValid || updRegionModel == null)
                {
                    return BadRequest();
                }

                var updRegion = _context.Regions.Where(x => x.ID == updRegionModel.ID).FirstOrDefault();

                if (updRegion != null)
                {
                    updRegion.ID = updRegionModel.ID;
                    updRegion.Name = updRegionModel.Name;
                    updRegion.CountryId = updRegionModel.CountryId;

                    _context.Update(updRegion);
                    _context.SaveChanges();
                }

                var updRegionResponse = new RegionViewModel()
                {
                    ID = updRegion.ID,
                    Name = updRegion.Name,
                    CountryId = updRegion.CountryId
                };

                return Ok(updRegionResponse);
            }
            
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return BadRequest();
            }

        }
    
        [HttpDelete ("{id:int}")]
        public IActionResult DeleteRegion (int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var delRegion = _context.Regions.Where(x => x.ID == id).FirstOrDefault();

                // wines also being deleted without needing to query as below:
                //var delRegion = _context.Regions.Include(x => x.Wines).Where(x => x.ID == id).FirstOrDefault();

                if (delRegion != null)
                {

                    _context.Remove(delRegion);
                    _context.SaveChanges();

                    Trace.TraceInformation($"The item {id} from {delRegion.Name} has been deleted");
                }

                return Ok();
            }
            
            catch (HttpRequestException ex)
            {
                Trace.TraceError(ex.Message);
                return BadRequest();
            }
 
            catch(SecurityException ex)
            {
                Trace.TraceError(ex.Message);
                return Forbid();
            }

            catch(Exception ex)
            {
                Trace.TraceError(ex.Message);
                return BadRequest();
            }
        }
    }
}