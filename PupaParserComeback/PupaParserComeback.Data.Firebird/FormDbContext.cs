namespace PupaParserComeback.Data.Firebird
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using FirebirdSql.Data.FirebirdClient;
    using Dto;

    public partial class FormDbContext : DbContext
    {
        public FormDbContext(string name, string initialCatalog = "")
            : base("name=" + name)
        {
            if (!string.IsNullOrWhiteSpace(initialCatalog))
            {
                var fbConnectionStrBldr = new FbConnectionStringBuilder(Database.Connection.ConnectionString);
                fbConnectionStrBldr.Database = initialCatalog;
                Database.Connection.ConnectionString = fbConnectionStrBldr.ConnectionString;
                Database.Connection.ConnectionString = Database.Connection.ConnectionString.Replace("data source=localhost", "data source=192.168.1.1");
            }
        }

        public virtual DbSet<DepartureKom> DepartureKom { get; set; }
        public virtual DbSet<DISMISS> DISMISS { get; set; }
        public virtual DbSet<DO_PRIZ> DO_PRIZ { get; set; }
        public virtual DbSet<DUP> DUP { get; set; }
        public virtual DbSet<GORODA> GORODA { get; set; }
        public virtual DbSet<IBE_LOG_TABLES> IBE_LOG_TABLES { get; set; }
        public virtual DbSet<IBE_VERSION_HISTORY> IBE_VERSION_HISTORY { get; set; }
        public virtual DbSet<KN_P> KN_P { get; set; }
        public virtual DbSet<kom> kom { get; set; }
        public virtual DbSet<LOG_P> LOG_P { get; set; }
        public virtual DbSet<NA_UCHETE> NA_UCHETE { get; set; }
        public virtual DbSet<NET_N_VB> NET_N_VB { get; set; }
        public virtual DbSet<NVOZVR> NVOZVR { get; set; }
        public virtual DbSet<OBRAZ> OBRAZ { get; set; }
        public virtual DbSet<OPERAT_SPR> OPERAT_SPR { get; set; }
        public virtual DbSet<PEC> PEC { get; set; }
        public virtual DbSet<PEC_STATE> PEC_STATE { get; set; }
        public virtual DbSet<PrintLog> PrintLog { get; set; }
        public virtual DbSet<PRIZ> PRIZ { get; set; }
        public virtual DbSet<RAILROAD> RAILROAD { get; set; }
        public virtual DbSet<REZH_KOM> REZH_KOM { get; set; }
        public virtual DbSet<rezhim> rezhim { get; set; }
        public virtual DbSet<RVK> RVK { get; set; }
        public virtual DbSet<SEM_POL> SEM_POL { get; set; }
        public virtual DbSet<SPEC> SPEC { get; set; }
        public virtual DbSet<SUD> SUD { get; set; }
        public virtual DbSet<V_OKRUG> V_OKRUG { get; set; }
        public virtual DbSet<VID_VS> VID_VS { get; set; }
        public virtual DbSet<VID_VS_FOR_PRIC> VID_VS_FOR_PRIC { get; set; }
        public virtual DbSet<ZVAN> ZVAN { get; set; }
        public virtual DbSet<IBE_LOG_BLOB_FIELDS> IBE_LOG_BLOB_FIELDS { get; set; }
        public virtual DbSet<IBE_LOG_FIELDS> IBE_LOG_FIELDS { get; set; }
        public virtual DbSet<IBE_LOG_KEYS> IBE_LOG_KEYS { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PRIZ>()
                .Property(e => e.GODN)
                .IsFixedLength();
        }
    }
}
