using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Wine.Commons.DAL.Interfaces;

namespace Wine.DataAccess
{
    public class CountryRepository : GenericRepository<Context>, ICountryRepository
    {
        public CountryRepository() : base()
        {

        }

        public CountryRepository(IConfiguration configuration) : base()
        {
            this.Context = new Context(configuration);
        }
    }
}
