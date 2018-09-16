using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wine.Commons.Business.Interfaces;
using Wine.Commons.Business.Models;
using Wine.Commons.CrossCutting;
using Wine.Commons.DAL.Interfaces;
using Wine.Data;

namespace Wine.Business.Services
{

    public class CountryService : ICountryService
    {
        private ICountryRepository _repository;

        public CountryService(ICountryRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<CountryModel>> GetAllCountries()
        {
            List<CountryModel> AllCountries = new List<CountryModel>();

            var allCountries = _repository.All<Country>();

            foreach (Country country in allCountries)
            {
                var response = new CountryModel()
                {
                    ID = country.ID,
                    Name = country.Name,
                    Regions = country.Regions?.Select(x => new RegionModel
                    {
                        ID = x.ID,
                        Name = x.Name,
                        CountryId = x.CountryId

                    }).ToList()
                };

                AllCountries.Add(response);
            }            

            return AllCountries;
        }

        public async Task<CountryModel> GetCountryById(int id)
        {
            var getCountryId = await _repository.GetSingleAsync<Wine.Data.Country>(x => x.ID == id);

            var output = new CountryModel
            {
                ID = getCountryId.ID,
                Name = getCountryId.Name,
                Regions = getCountryId.Regions?.Select(x => new RegionModel
                {
                    ID = x.ID,
                    Name = x.Name,
                    CountryId = x.CountryId

                }).ToList()
            };

            return output;
        }

        public async Task<CountryModel> GetCountryByName(string countryName)
        {
            var getCountryName = await _repository.GetSingleAsync<Wine.Data.Country>(x => x.Name == countryName);

            var displayCountry = new CountryModel()
            {
                ID = getCountryName.ID,
                Name = getCountryName.Name,
                Regions = getCountryName.Regions?.Select(x => new RegionModel
                {
                    ID = x.ID,
                    Name = x.Name,
                    CountryId = x.CountryId                     

                }).ToList() 
                
            };      

            return displayCountry;
        }

        public async Task<CountryModel> AddNewCountry(CountryModel newModel)
        {
            var addCountry = _repository.Add<Wine.Data.Country>(new Wine.Data.Country
            {
                Name = newModel.Name
            });

            await _repository.CommitAsync();

            //newModel.ID = addCountry.ID;
            //newModel.Name = addCountry.Name;            

            //return newModel;

            return (CountryModel)Mapper.UpdateMapper(addCountry, newModel);
        }

        public async Task<CountryModel> UpdateCountry(CountryModel updModel)
        {
            var updCountry = await _repository.GetSingleAsync<Wine.Data.Country>(x => x.Name == updModel.Name);       

            updCountry.Name = updModel.Name;

            _repository.Update<Wine.Data.Country>(updCountry);

            await _repository.CommitAsync();

            return updModel;
        }

        public async Task<bool> DeleteCountryById(int id)
        {
            var delCountry = await _repository.GetSingleAsync<Wine.Data.Country>(x => x.ID == id, x => x.Regions);

            _repository.DeleteMany<Wine.Data.Region>(delCountry.Regions.ToArray());

            _repository.Delete<Wine.Data.Country>(delCountry);

            await _repository.CommitAsync();

            return true;
        }

        public async Task<bool> DeleteCountryByName(string name)
        {
            var delCountry = await _repository.GetSingleAsync<Wine.Data.Country>(x => x.Name == name, x => x.Regions);

            _repository.DeleteMany<Wine.Data.Region>(delCountry.Regions.ToArray());

            _repository.Delete<Wine.Data.Country>(delCountry);

            await _repository.CommitAsync();

            return true;
        }
    }
}
