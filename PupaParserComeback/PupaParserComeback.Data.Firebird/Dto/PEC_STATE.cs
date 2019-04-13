namespace PupaParserComeback.Data.Firebird.Dto
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Firebird.PEC_STATE")]
    public partial class PEC_STATE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int STATE_ID { get; set; }

        [StringLength(20)]
        public string NAME { get; set; }
    }
}
