namespace PupaParserComeback.Data.SQLite.Dto
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("log")]
    public partial class log
    {
        public long id { get; set; }

        public string date { get; set; }

        public string time { get; set; }

        [StringLength(2147483647)]
        public string action { get; set; }

        [StringLength(2147483647)]
        public string hostname { get; set; }
    }
}
