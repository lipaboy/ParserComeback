using System.Collections;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace PupaParserComeback.Data.Firebird.ExtensionMethods
{
    public static class FirebirdDbContextExtensions
    {
        private class IdResult
        {
            public int Id { get; set; }
        }

        public static int NextId(this DbContext dbContext, string generatorName)
        {
            string sql = $"SELECT NEXT VALUE FOR {generatorName} AS Id FROM RDB$DATABASE";
            return dbContext.Database.SqlQuery<IdResult>(sql).First().Id;
        }

        public static void Refresh(this DbContext dbContext, RefreshMode mode, object entity)
        {
            var objectContext = ((IObjectContextAdapter)dbContext).ObjectContext;
            objectContext.Refresh(RefreshMode.StoreWins, entity);
        }

        public static void Refresh(this DbContext dbContext, RefreshMode mode, IEnumerable collection)
        {
            var objectContext = ((IObjectContextAdapter)dbContext).ObjectContext;
            objectContext.Refresh(RefreshMode.StoreWins, collection);
        }

        public static void ClearTable(this DbContext dbContext, string tableName)
        {
            dbContext.Database.ExecuteSqlCommand($"DELETE FROM {tableName};");
        }

        public static void ClearGenerators(this DbContext dbContext, string generator)
        {
            dbContext.Database.ExecuteSqlCommand($"SET GENERATOR {generator} TO 0");
        }
    }
}
