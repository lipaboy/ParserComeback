using PupaParserComeback.Data.Firebird.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PupaParserComeback.Data.Firebird.Test
{
    public class DbContextTestFactory : IDbContextFactory
    {
        private string _connectionStringName;

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
            return new FormDbContext(_connectionStringName);
        }
    }
}
