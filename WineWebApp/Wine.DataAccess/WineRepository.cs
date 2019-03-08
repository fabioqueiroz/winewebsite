using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Wine.Commons.DAL.Interfaces;

namespace Wine.DataAccess
{
    public class WineRepository : GenericRepository<Context>, IWineRepository, IDisposable
    {
        public WineRepository() : base()
        {

        }

        public WineRepository(IConfiguration configuration) : base()
        {
            this.Context = new Context(configuration);
        }

        protected virtual void Dispose()
        {
            this.Context.Dispose();
        }

        ~WineRepository() 
        {
            this.Context.Dispose();
        }
    }
}
