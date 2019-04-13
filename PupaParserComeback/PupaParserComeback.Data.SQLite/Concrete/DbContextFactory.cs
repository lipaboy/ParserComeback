using PupaParserComeback.Data.SQLite.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PupaParserComeback.Data.SQLite.Concrete
{
    public class DbContextFactory : IDbContextFactory
    {
        private string _connectionStringName;
        private string _fileName;

        public DbContextFactory(string connectionStringName, string fileName)
        {
            if (string.IsNullOrWhiteSpace(connectionStringName))
            {
                throw new ArgumentNullException(nameof(connectionStringName));
            }

            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            _connectionStringName = connectionStringName;
            _fileName = fileName;
        }

        public DbContext Create()
        {
            return new PupaDbContext(_connectionStringName, _fileName);
        }
    }
}
