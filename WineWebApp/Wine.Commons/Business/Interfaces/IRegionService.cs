using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wine.Commons.Business.Models;

namespace Wine.Commons.Business.Interfaces
{
    public interface IRegionService
    {       
        Task<List<RegionModel>> GetAllRegions();

        Task<RegionModel> GetRegionByName(string regionName);

        Task<RegionModel> AddNewRegion(RegionModel newModel);

        Task<RegionModel> UpdateRegion(RegionModel updModel);

        Task<bool> DeleteRegionById(int id);

        Task<bool> DeleteRegionByName(string name);
    }
}
