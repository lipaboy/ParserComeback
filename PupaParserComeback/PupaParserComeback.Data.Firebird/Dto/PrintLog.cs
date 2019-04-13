namespace PupaParserComeback.Data.Firebird.Dto
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Firebird.PrintLog")]
    public partial class PrintLog
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Required]
        [StringLength(63)]
        public string Type { get; set; }

        [Required]
        [StringLength(25)]
        public string N_KOM { get; set; }

        public DateTime? TIMESTAMP { get; set; }
    }
}
