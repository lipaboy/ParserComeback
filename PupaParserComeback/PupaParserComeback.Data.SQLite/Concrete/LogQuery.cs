using PupaParserComeback.Data.SQLite.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PupaParserComeback.Data.SQLite.Dto;

namespace PupaParserComeback.Data.SQLite.Concrete
{
    public class LogQuery : ILogQuery
    {
        private readonly IDbContextFactory _dbContextFactory;

        public LogQuery(IDbContextFactory dbContextFactory)
        {
            if (dbContextFactory == null)
            {
                throw new ArgumentNullException(nameof(dbContextFactory));
            }

            _dbContextFactory = dbContextFactory;
        }

        public log Get(long id)
        {
            using (var dbContext = _dbContextFactory.Create())
            {
                return dbContext.Set<log>().Find(id);
            }
        }
    }
}
