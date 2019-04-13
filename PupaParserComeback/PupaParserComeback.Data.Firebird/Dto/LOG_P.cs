namespace PupaParserComeback.Data.Firebird.Dto
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Firebird.LOG_P")]
    public partial class LOG_P
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public int? KOD { get; set; }

        [StringLength(26)]
        public string O_N_KOM { get; set; }

        [StringLength(26)]
        public string N_N_KOM { get; set; }

        [Column(TypeName = "date")]
        public DateTime? D_IZM { get; set; }

        public TimeSpan? T_IZM { get; set; }
    }
}
