using PupaParserComeback.Data.Firebird.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace PupaParserComeback.Data.Firebird.Concrete
{
    public class DbContextFactory : IDbContextFactory
    {
        private readonly string _connectionStringName;
        private readonly string _initialCatalog;

        public DbContextFactory(string connectionStringName, string initialCatalog)
        {
            if (string.IsNullOrWhiteSpace(connectionStringName))
            {
                throw new ArgumentNullException(nameof(connectionStringName));
            }

            if (string.IsNullOrWhiteSpace(initialCatalog))
            {
                throw new ArgumentNullException(nameof(initialCatalog));
            }

            _connectionStringName = connectionStringName;
            _initialCatalog = initialCatalog;
        }

        public DbContext Create()
        {
            return new FormDbContext(_connectionStringName, _initialCatalog);
        }
    }
}
