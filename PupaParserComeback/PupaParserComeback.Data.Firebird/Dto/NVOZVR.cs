namespace PupaParserComeback.Data.Firebird.Dto
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Firebird.NVOZVR")]
    public partial class NVOZVR
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [StringLength(30)]
        public string FAM { get; set; }

        [StringLength(30)]
        public string IM { get; set; }

        [StringLength(30)]
        public string OTCH { get; set; }

        [Column(TypeName = "date")]
        public DateTime? D_ROD { get; set; }

        [StringLength(30)]
        public string RVK { get; set; }

        [StringLength(30)]
        public string PRICH { get; set; }

        [StringLength(30)]
        public string PRIM { get; set; }

        [Column(TypeName = "date")]
        public DateTime? D_UBIT { get; set; }

        public int? OLD_KOD { get; set; }
    }
}
