using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PupaParserComeback.Data.SQLite.ExtensionMethods
{
    public static class DbContextExtensions
    {
        public static void ClearTable(this DbContext source, string tableName)
        {
            source.Database.ExecuteSqlCommand($"delete from {tableName};");
            source.Database.ExecuteSqlCommand("delete from sqlite_sequence where name = @tableName;",
                new SQLiteParameter("tableName", tableName));
        }
    }
}
