using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Wine.Commons.DAL.Interfaces;

namespace Wine.DataAccess
{
    public class RegionRepository : GenericRepository<Context>, IRegionRepository
    {
        public RegionRepository() : base()
        {

        }

        public RegionRepository(IConfiguration configuration) : base()
        {
            this.Context = new Context(configuration);
        }
    }
}
