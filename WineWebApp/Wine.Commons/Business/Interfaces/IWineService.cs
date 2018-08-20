using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wine.Commons.Business.Models;

namespace Wine.Commons.Business.Interfaces
{
    public interface IWineService 
    {        
        Task<List<WineModel>> GetAllWines();

        Task<WineModel> GetWineById(int id);

        Task<WineModel> AddNewWine(WineModel newModel);

        Task<WineModel> UpdateWineById(WineModel updModel);

        Task<bool> DeleteWineById(int id);
    }
}
