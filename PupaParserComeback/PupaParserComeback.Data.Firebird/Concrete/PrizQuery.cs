using PupaParserComeback.Data.Firebird.Abstract;
using PupaParserComeback.Data.Firebird.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PupaParserComeback.Data.Firebird.Concrete
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

        public IEnumerable<PRIZ> GetAll()
        {
            using (var dbContext = _dbContextFactory.Create())
            {
                return dbContext.Set<PRIZ>().ToList();
            }
        }

        public PRIZ Get(int id)
        {
            using (var dbContext = _dbContextFactory.Create())
            {
                return dbContext.Set<PRIZ>().Find(id);
            }
        }
    }
}
