using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wine.Commons.Business.Interfaces;
using Wine.Commons.Business.Models;
using WineWebApp.ViewModels;

namespace WineWebApp.Controllers
{
    [Route("countries")]
    public class CountriesController : Controller
    {
        private ICountryService _countryService;

        public CountriesController(ICountryService countryservice)
        {
            _countryService = countryservice;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var countries = await _countryService.GetAllCountries();

                var countryViewModel = countries?.Select(x => new CountryViewModel
                {
                    ID = x.ID,
                    Name = x.Name,
                    Regions = x.Regions?.Select(y => new RegionViewModel
                    {
                         ID = y.ID,
                         Name = y.Name,   
                         CountryID = y.CountryID

                    }).ToList()

                }).ToList();

                return View(countryViewModel);
            }
            catch (WebException ex)
            {
                return View();
            }
           
        }

        // shows the edit page to update an entry
        [HttpGet("{countryName}")]
        public async Task<IActionResult> Edit(string countryName)
        {
            var getCountry = await _countryService.GetCountryByName(countryName);

            var output = new CountryViewModel
            {
                ID = getCountry.ID,
                Name = getCountry.Name,
                Regions = getCountry.Regions?.Select(x => new RegionViewModel
                {
                    ID = x.ID,
                    Name = x.Name,
                    CountryID = x.CountryID

                }).ToList()

            };

            return View(output);
        }

        // shows the delete confirmation page
        [HttpGet("DeleteCountryById/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var getCountry = await _countryService.GetCountryById(id);

            var output = new CountryViewModel
            {
                ID = getCountry.ID,
                Name = getCountry.Name,
                Regions = getCountry.Regions?.Select(x => new RegionViewModel
                {
                    ID = x.ID,
                    Name = x.Name,
                    CountryID = x.CountryID

                }).ToList()

            };

            return View(output);
        }

        [HttpPost]
        public async Task<IActionResult> AddCountry([FromBody]CountryViewModel newCountry)
        {
            try
            {
                if (!ModelState.IsValid || newCountry == null)
                {
                    return BadRequest();
                }

                var addCountry = new CountryModel
                {
                    Name = newCountry.Name
                };

                await _countryService.AddNewCountry(addCountry);

                var output = new CountryViewModel
                {
                    ID = addCountry.ID,
                    Name = addCountry.Name
                };

                return View(output);
            }
            catch (WebException)
            {
                return View();
            }
        }

        // updates entry
        [HttpPost("editcountry")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("ID, Name")]CountryViewModel updCountry)
        {
            try
            {
                if (!ModelState.IsValid || updCountry == null)
                {
                    return BadRequest();
                }

                var findCountry = await _countryService.GetCountryByName(updCountry.Name);

                findCountry.ID = updCountry.ID;
                findCountry.Name = updCountry.Name;

                await _countryService.UpdateCountry(findCountry);
               
            } 
            catch (WebException)
            {
                return View();
            }

            return RedirectToAction("Index", "countries");

        }

        // deletes entry
        [HttpPost("DeleteCountryById")]
        public async Task<IActionResult> DeleteCountryById([Bind("ID")]CountryViewModel model)
        {
            try
            {
                var getCountry = await _countryService.GetCountryById(model.ID);

                await _countryService.DeleteCountryById(getCountry.ID);
            }
            catch (Exception)
            {

                return View();
            }

            return RedirectToAction("Index", "countries");

        }
    }
}