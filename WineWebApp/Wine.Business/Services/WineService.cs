using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wine.Commons.Business.Interfaces;
using Wine.Commons.Business.Models;
using Wine.Commons.DAL.Interfaces;

namespace Wine.Business.Services
{
    public class WineService : IWineService
    {
        private IWineRepository _repository;
        public WineService(IWineRepository repository)
        {
            _repository = repository;
        }

        // get all
        public async Task<List<WineModel>> GetAllWines()
        {
            List<WineModel> allWines = new List<WineModel>();

            var getAll = _repository.All<Wine.Data.Wine>();

            foreach (Wine.Data.Wine wine in getAll)
            {
                var response = new WineModel()
                {
                    ID = wine.ID,
                    Name = wine.Name,
                    RegionID = wine.RegionId,
                    Price = wine.Price,
                    Description = wine.Description,
                    Sparkling = wine.Sparkling
                };

                allWines.Add(response);
            }

            return allWines;          
        }

        // get one 
        public async Task<WineModel> GetWineById(int id)
        {
            var wine = await _repository.GetSingleAsync<Wine.Data.Wine>(x => x.ID == id);

            return new WineModel { ID = wine.ID, Name = wine.Name, Price = wine.Price, Description = wine.Description, RegionID = wine.RegionId, Sparkling = wine.Sparkling };
        }

        // add a new wine
        public async Task<WineModel> AddNewWine(WineModel newModel)
        {
            var addWine = _repository.Add<Wine.Data.Wine>(new Wine.Data.Wine
            {
                Name = newModel.Name,
                RegionId = newModel.RegionID,
                Price = newModel.Price,
                Description = newModel.Description,
                Sparkling = newModel.Sparkling
                
            });

            await _repository.CommitAsync();

            newModel.ID = addWine.ID;
            newModel.Name = addWine.Name;
            newModel.RegionID = addWine.RegionId;
            newModel.Price = addWine.Price;
            newModel.Description = addWine.Description;
            newModel.Sparkling = addWine.Sparkling;

            return newModel;
        }

        // update 
        public async Task<WineModel> UpdateWineById(WineModel updModel)
        {
            var findModel = await _repository.GetSingleAsync<Wine.Data.Wine>(x => x.ID == updModel.ID);

            findModel.Name = updModel.Name;
            findModel.RegionId = updModel.RegionID;
            findModel.Price = updModel.Price;
            findModel.Description = updModel.Description;
            findModel.Sparkling = updModel.Sparkling;

            _repository.Update<Wine.Data.Wine>(findModel);

            await _repository.CommitAsync();

            return updModel;
        }

        // delete
        public async Task<bool> DeleteWineById(int id)
        {
            var delWine = await _repository.GetSingleAsync<Wine.Data.Wine>(x => x.ID == id);

            _repository.Delete<Wine.Data.Wine>(delWine);
          
            await _repository.CommitAsync();

            return true;
        }
        
    }
}
