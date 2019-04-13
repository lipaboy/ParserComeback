using PupaParserComeback.Data.SQLite.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PupaParserComeback.Data.SQLite.Dto;
using System.Data.Entity;

namespace PupaParserComeback.Data.SQLite.Concrete
{
    public class PrizQuery : IPrizQuery
    {
        private readonly IDbContextFactory _dbContextFactory;

        public PrizQuery(IDbContextFactory dbContextFactory)
        {
            if (dbContextFactory == null)
            {
                throw new ArgumentNullException(nameof(dbContextFactory));
            }

            _dbContextFactory = dbContextFactory;
        }

        public IEnumerable<priz> GetAll()
        {
            using (var dbContext = _dbContextFactory.Create())
            {
                return dbContext.Set<priz>().ToList();
            }
        }

        public priz Get(long id)
        {
            using (var dbContext = _dbContextFactory.Create())
            {
                return dbContext.Set<priz>().Find(id);
            }
        }

        public IEnumerable<priz> Get(IEnumerable<long> ids)
        {
            using (var dbContext = _dbContextFactory.Create())
            {
                return dbContext.Set<priz>().Where(p => ids.Contains(p.id)).ToList();
            }
        }
    }
}
