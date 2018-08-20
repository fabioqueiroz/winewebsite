using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wine.Commons.Business.Models;

namespace Wine.Commons.Business.Interfaces
{
    public interface ICountryService
    {
        Task<List<CountryModel>> GetAllCountries();

        Task<CountryModel> GetCountryById(int id);

        Task<CountryModel> GetCountryByName(string countryName);

        Task<CountryModel> AddNewCountry(CountryModel newModel);

        Task<CountryModel> UpdateCountry(CountryModel updModel);

        Task<bool> DeleteCountryById(int id);

        Task<bool> DeleteCountryByName(string name);
    }
}
