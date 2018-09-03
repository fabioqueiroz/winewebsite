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
    [Route ("wines")]
    public class WinesController : Controller
    {
        private IWineService _wineService;

        public WinesController(IWineService wineService)
        {
            _wineService = wineService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            try
            {
                var wines = await _wineService.GetAllWines();

                var wineViewModel = wines?.Select(x => new WineViewModel
                {
                    ID = x.ID,
                    Name = x.Name,
                    RegionId = x.RegionId,
                    Description = x.Description,
                    Price = x.Price,
                    Sparkling = x.Sparkling

                }).ToList();

                return View(wineViewModel);
            }
            catch (WebException ex)
            {
                return View();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var findWine = await _wineService.GetWineById(id);

                var output = new WineViewModel
                {
                    ID = findWine.ID,
                    Name = findWine.Name,
                    RegionId = findWine.RegionId,
                    Description = findWine.Description,
                    Price = findWine.Price,
                    Sparkling = findWine.Sparkling
                };

                return View(output);
            }
            catch (WebException)
            {
                return View();
            }
        }

        [HttpGet("DeleteWineById/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var findWine = await _wineService.GetWineById(id);

                var output = new WineViewModel
                {
                    ID = findWine.ID,
                    Name = findWine.Name,
                    RegionId = findWine.RegionId,
                    Description = findWine.Description,
                    Price = findWine.Price,
                    Sparkling = findWine.Sparkling
                };

                return View(output);
            }
            catch (WebException)
            {
                return View();
            }
        }

        [HttpGet("AddWine")]
        public async Task<IActionResult> Create() => View(new WineViewModel());

        [HttpPost("AddWine")]
        public async Task<IActionResult> AddWine([Bind("ID, Name, Description, Price, Sparkling, RegionId")]WineViewModel newEntry)
        {
            try
            {
                if (!ModelState.IsValid || newEntry == null)
                {
                    return BadRequest();
                }

                var addWine = new WineModel
                {
                    Name = newEntry.Name,
                    RegionId = newEntry.RegionId,
                    Description = newEntry.Description,
                    Price = newEntry.Price,
                    Sparkling = newEntry.Sparkling
                };

                await _wineService.AddNewWine(addWine);

                var output = new WineViewModel
                {
                    ID = addWine.ID,
                    Name = addWine.Name,
                    RegionId = addWine.RegionId,
                    Description = addWine.Description,
                    Price = addWine.Price,
                    Sparkling = addWine.Sparkling
                };

            }
            catch (WebException)
            {
                return View();
            }

            return RedirectToAction("Index", "wines");
        }

        [HttpPost("editwine")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("ID, Name, Description, Price, Sparkling, RegionId")]WineViewModel updEntry)
        {
            try
            {
                if (!ModelState.IsValid || updEntry == null)
                {
                    return BadRequest();
                }

                var findWine = await _wineService.GetWineById(updEntry.ID);

                findWine.ID = updEntry.ID;
                findWine.Name = updEntry.Name;
                findWine.RegionId = updEntry.RegionId;
                findWine.Description = updEntry.Description;
                findWine.Price = updEntry.Price;
                findWine.Sparkling = updEntry.Sparkling;

                await _wineService.UpdateWineById(findWine);

            }
            catch (WebException)
            {
                return View();
            }

            return RedirectToAction("Index", "wines");
        }

        [HttpPost("DeleteWineById")]
        public async Task<IActionResult> DeleteWineById([Bind("ID")]WineViewModel wineModel)
        {
            try
            {
                var findWine = await _wineService.GetWineById(wineModel.ID);

                await _wineService.DeleteWineById(findWine.ID);

            }
            catch (WebException)
            {
                return View();
            }

            return RedirectToAction("Index", "wines");
        }
    }
}
