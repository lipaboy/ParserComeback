namespace PupaParserComeback.Data.Firebird.Dto
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Firebird.IBE$LOG_FIELDS")]
    public partial class IBE_LOG_FIELDS
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LOG_TABLES_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(31)]
        public string FIELD_NAME { get; set; }

        [StringLength(255)]
        public string OLD_VALUE { get; set; }

        [StringLength(255)]
        public string NEW_VALUE { get; set; }
    }
}
