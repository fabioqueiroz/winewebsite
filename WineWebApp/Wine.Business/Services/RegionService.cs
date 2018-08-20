using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wine.Commons.Business.Interfaces;
using Wine.Commons.Business.Models;
using Wine.Commons.DAL.Interfaces;

namespace Wine.Business.Services
{
    public class RegionService : IRegionService
    {
        private IWineRepository _repository;
        public RegionService(IWineRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<RegionModel>> GetAllRegions()
        {
            List<RegionModel> AllRegions = new List<RegionModel>();

            var getAllRegions = _repository.All<Wine.Data.Region>();

            foreach (Wine.Data.Region region in getAllRegions)
            {
                var response = new RegionModel()
                {
                    ID = region.ID,
                    Name = region.Name,
                    CountryID = region.CountryID,
                    Wines = region.Wines?.Select(x => new WineModel
                    {
                        ID = x.ID,
                        Name = x.Name,
                        Description = x.Description,
                        Price = x.Price,
                        RegionID = x.RegionId

                    }).ToList()
                };

                AllRegions.Add(response);

                //var response2 = new RegionModel();

                //if (response2.Wines != null)
                //{
                //    foreach (Wine.Data.Region item in getAllRegions)
                //    {
                //        response2.Name = item.Name;
                //        response2.CountryID = item.CountryID;

                //        foreach (Wine.Data.Wine wine in item.Wines)
                //        {
                //            response2.Wines.Add(new WineModel
                //            {
                //                ID = wine.ID,
                //                Name = wine.Name,
                //                Description = wine.Description,
                //                Price = wine.Price
                //            });
                //        }
                //    }
                //}
                //AllRegions.Add(response2);
            }

            return AllRegions;
        }

        public async Task<RegionModel> GetRegionByName(string regionName)
        {
            var region = await _repository.GetSingleAsync<Wine.Data.Region>(x => x.Name == regionName);

            var displayRegion = new RegionModel()
            {
                ID = region.ID,
                Name = region.Name,
                CountryID = region.CountryID,
                Wines = region.Wines?.Select(x => new WineModel
                {
                    ID = x.ID,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    RegionID = x.RegionId

                }).ToList()
            };

            return displayRegion;
        }

        public async Task<RegionModel> AddNewRegion(RegionModel newModel)
        {
            var addRegion = _repository.Add<Wine.Data.Region>(new Wine.Data.Region
            {
              // ID will be created automatically here
                Name = newModel.Name,
                CountryID = newModel.CountryID               
            });

            await _repository.CommitAsync();

            newModel.ID = addRegion.ID; // at this point the ID will already be generated and has to be sent back
            newModel.Name = addRegion.Name;
            newModel.CountryID = addRegion.CountryID;

            return newModel;
        }

        public async Task<RegionModel> UpdateRegion(RegionModel updModel)
        {
            var updRegion = await _repository.GetSingleAsync<Wine.Data.Region>(x => x.Name == updModel.Name);

            updRegion.Name = updModel.Name;
            updRegion.CountryID = updModel.CountryID;

            _repository.Update<Wine.Data.Region>(updRegion);

            await _repository.CommitAsync();

            //var response = new RegionModel()
            //{
            //    ID = updRegion.ID,
            //    Name = updRegion.Name,
            //    CountryID = updRegion.CountryID
            //};
           
            return updModel;
        }

        public async Task<bool> DeleteRegionById(int id)
        {
            var delRegion = await _repository.GetSingleAsync<Wine.Data.Region>(x => x.ID == id, x => x.Wines);

            _repository.DeleteMany<Wine.Data.Wine>(delRegion.Wines.ToArray());

            _repository.Delete<Wine.Data.Region>(delRegion);

            await _repository.CommitAsync();

            return true;
        }

        public async Task<bool> DeleteRegionByName(string name)
        {         
            var delRegionName = await _repository.GetSingleAsync<Wine.Data.Region>(x => x.Name == name, x => x.Wines);

            _repository.DeleteMany<Wine.Data.Wine>(delRegionName.Wines.ToArray());

            _repository.Delete<Wine.Data.Region>(delRegionName);

            await _repository.CommitAsync();

            return true;
        }
    }
}
