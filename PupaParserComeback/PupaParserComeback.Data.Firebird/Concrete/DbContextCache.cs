using PupaParserComeback.Data.Firebird.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PupaParserComeback.Data.Firebird.Concrete
{
    public class DbContextCache : IDbContextCache
    {
        private readonly IDbContextFactory _dbContextFactory;
        private DbContext _dbContext;

        public DbContextCache(IDbContextFactory dbContextFactory)
        {
            if (dbContextFactory == null)
            {
                throw new ArgumentNullException(nameof(dbContextFactory));
            }

            _dbContextFactory = dbContextFactory;
        }

        public DbContext Create()
        {
            _dbContext = _dbContextFactory.Create();

            return _dbContext;
        }

        public DbContext Context
        {
            get
            {
                if (_dbContext == null)
                {
                    throw new InvalidOperationException("Context is not created");
                }

                return _dbContext;
            }
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            _dbContext = null;
        }
    }
}
