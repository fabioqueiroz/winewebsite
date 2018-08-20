using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wine.WebAPI.ViewModels;
using Wine.WebAPI.Models;
using System.Net.Http;
using System.Security;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace Wine.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/country")]
    public class CountryController : Controller
    {
        private Context _context;
        public CountryController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public JsonResult GetCountry()
        {
            return new JsonResult(new List<object>
            {
                new {id = 1, name = "rioja", country = "spain"},
                new {id = 2, name = "verdejo", country = "spain"},
                new {id = 3, name = "barolo", country = "italy"},
                new {id = 4, name = "falanghina", country = "italy"}
            });
        }

        [HttpGet ("{country}")]
        public IActionResult GetCountry (string country)
        {
            try
            {
                if (country == null)
                {
                    return BadRequest();
                }

                var countryModel = _context.Countries.Where(x => x.Name == country).FirstOrDefault();

                var countryResponse = new CountryViewModel()
                {
                    Id = countryModel.ID,
                    Name = countryModel.Name
                };

                return Ok(countryResponse);
            }

            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return BadRequest();
            }
        }

        [HttpGet("regions/{country}")]
        public IActionResult GetRegionCountry(string country)
        {
            try
            {
                if (country == null)
                {
                    return BadRequest();
                }

                var countrySelectModel = _context.Countries.Include(x => x.Regions).FirstOrDefault(x => x.Name == country);

                var countryResponse = new CountryViewModel()
                {
                    Id = countrySelectModel.ID,
                    Name = countrySelectModel.Name,
                    Regions = countrySelectModel.Regions != null ? countrySelectModel.Regions.Select(x => new RegionViewModel
                    {
                        Name = x.Name

                    }).ToList(): null
                };

                return Ok(countryResponse);
            }

            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult AddCountry ([FromBody]CountryAddViewModel countryModel)
        {
            try
            {
                if (!ModelState.IsValid || countryModel == null)
                {
                    return BadRequest();
                }

                var addCountry = new Wine.Data.Country()
                {
                    Name = countryModel.Name
                };

                if (addCountry != null)
                {
                    _context.Add(addCountry);
                    _context.SaveChanges();
                }

                var countryResponse = new CountryViewModel()
                {
                    Id = addCountry.ID,
                    Name = addCountry.Name
                };

                return Ok(countryResponse);
            }
            
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult UpdateCountry([FromBody]CountryUpdateViewModel updCountryModel)
        {
            try
            {
                if (!ModelState.IsValid || updCountryModel == null)
                {
                    return BadRequest();
                }

                var updCountry = _context.Countries.Where(x => x.ID == updCountryModel.Id).FirstOrDefault();

                if (updCountry != null)
                {
                    updCountry.ID = updCountryModel.Id;
                    updCountry.Name = updCountryModel.Name;

                    _context.Update(updCountry);
                    _context.SaveChanges();
                }

                var updCountryResponse = new CountryViewModel()
                {
                    Id = updCountry.ID,
                    Name = updCountry.Name
                };

                return Ok(updCountryResponse);
            }
            
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return BadRequest();
            }
        }
        
        [HttpDelete("{id:int}")]
        public IActionResult DeleteCountry(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var delCountry = _context.Countries.Where(x => x.ID == id).FirstOrDefault();

                if (delCountry != null)
                {
                    _context.Remove(delCountry);
                    _context.SaveChanges();

                    Trace.TraceInformation($"The country {id} {delCountry.Name} has been deleted");
                }

                return Ok();
            }
                
            catch (HttpRequestException ex)
            {
                Trace.TraceError(ex.Message);
                return BadRequest();
            }

            catch (SecurityException ex)
            {
                Trace.TraceError(ex.Message);
                return Forbid();
            }

            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return BadRequest();
            }
        }
    }
}