using PupaParserComeback.Data.SQLite.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace PupaParserComeback.Data.SQLite.Test
{
    public class DbContextTestFactory : IDbContextFactory
    {
        private readonly string _connectionStringName;

        public DbContextTestFactory(string connectionStringName)
        {
            if (string.IsNullOrWhiteSpace(connectionStringName))
            {
                throw new ArgumentNullException(nameof(connectionStringName));
            }

            _connectionStringName = connectionStringName;
        }

        public DbContext Create()
        {
            return new PupaDbContext(_connectionStringName);
        }
    }
}
