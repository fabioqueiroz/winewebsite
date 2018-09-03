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
    [Route("regions")]
    public class RegionsController : Controller
    {
        private IRegionService _regionService;

        public RegionsController(IRegionService regionService)
        {
            _regionService = regionService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            try
            {
                var regions = await _regionService.GetAllRegions();

                var regionViewModel = regions?.Select(x => new RegionViewModel
                {
                    ID = x.ID,
                    Name = x.Name,
                    CountryId = x.CountryId,
                    Wines = x.Wines?.Select(y => new WineViewModel
                    {
                        ID = y.ID,
                        Name = y.Name,
                        Description = y.Description,
                        Price = y.Price

                    }).ToList()

                }).ToList();

                return View(regionViewModel);
            }
            catch (WebException ex)
            {
                return View();
            }
        }


        [HttpGet("{name}")]
        public async Task<IActionResult> Edit(string name)
        {
            try
            {
                var findRegion = await _regionService.GetRegionByName(name);

                var output = new RegionViewModel
                {
                    ID = findRegion.ID,
                    Name = findRegion.Name,
                    CountryId = findRegion.CountryId,
                    Wines = findRegion.Wines?.Select(x => new WineViewModel
                    {
                        ID = x.ID,
                        Name = x.Name,
                        Description = x.Description,
                        Price = x.Price

                    }).ToList()
                };

                return View(output);
            }
            catch (WebException)
            {
                return View();
            }
        }

        [HttpGet("DeleteRegionByName/{name}")]
        public async Task<IActionResult> Delete(string name)
        {
            try
            {
                var findRegion = await _regionService.GetRegionByName(name);

                var output = new RegionViewModel
                {
                    ID = findRegion.ID,
                    Name = findRegion.Name,
                    CountryId = findRegion.CountryId,
                    Wines = findRegion.Wines?.Select(x => new WineViewModel
                    {
                        ID = x.ID,
                        Name = x.Name,
                        Description = x.Description,
                        Price = x.Price

                    }).ToList()
                };

                return View(output);
            }
            catch (WebException)
            {
                return View();
            }
        }

        [HttpGet("AddRegion")]
        public async Task<IActionResult> Create() => View(new RegionViewModel());


        [HttpPost("AddRegion")]
        public async Task<IActionResult> AddRegion([Bind("ID", "Name, CountryId")]RegionViewModel regionModel)
        {
            try
            {
                if (!ModelState.IsValid || regionModel == null)
                {
                    return BadRequest();
                }

                var addNewRegion = new RegionModel
                {
                    Name = regionModel.Name,
                    CountryId = regionModel.CountryId
                };

                await _regionService.AddNewRegion(addNewRegion);

                var output = new RegionViewModel
                {
                    ID = addNewRegion.ID,
                    Name = addNewRegion.Name,
                    CountryId = addNewRegion.CountryId
                };
               
            }
            catch (WebException)
            {
                return View();
            }

            return RedirectToAction("Index", "regions");
        }

        [HttpPost("editregion")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("ID, Name, CountryId")]RegionViewModel updRegion)
        {
            try
            {
                if (!ModelState.IsValid || updRegion == null)
                {
                    return BadRequest();
                }

                var findRegion = await _regionService.GetRegionByName(updRegion.Name);

                findRegion.ID = updRegion.ID;
                findRegion.Name = updRegion.Name;
                findRegion.CountryId = updRegion.CountryId;

                await _regionService.UpdateRegion(findRegion);

            }
            catch (WebException)
            {
                return View();
            }

            return RedirectToAction("Index", "regions");
        }

        [HttpPost("DeleteRegionByName")]
        public async Task<IActionResult> DeleteRegionByName([Bind("Name")]RegionViewModel regionModel)
        {
            try
            {
                var findRegion = await _regionService.GetRegionByName(regionModel.Name);

                await _regionService.DeleteRegionByName(findRegion.Name);

            }
            catch (WebException)
            {
                return View();
            }

            return RedirectToAction("Index", "regions");
        }
    }
}
