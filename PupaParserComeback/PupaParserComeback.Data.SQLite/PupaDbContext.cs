namespace PupaParserComeback.Data.SQLite
{
    using System.Data.Entity;
    using System.Data.SQLite;
    using Dto;

    public partial class PupaDbContext : DbContext
    {
        public PupaDbContext(string name, string fileName) : base(
            new SQLiteConnection()
            {
                ConnectionString = new SQLiteConnectionStringBuilder()
                {
                    DataSource = fileName,
                    ForeignKeys = true
                }.ConnectionString
            },
            contextOwnsConnection: true)
        {
        }

        public PupaDbContext(string name)
            : base("name=" + name)
        {
        }

        public virtual DbSet<log> logs { get; set; }
        public virtual DbSet<priz> prizs { get; set; }
    }
}
