namespace PupaParserComeback.Data.Firebird.Dto
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Firebird.PEC")]
    public partial class PEC
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int N_CARD { get; set; }

        public int? ID_PRIZ { get; set; }

        public int? STATE { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PEC_DATE { get; set; }

        public TimeSpan? PEC_TIME { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PEC_VIDAN { get; set; }
    }
}
