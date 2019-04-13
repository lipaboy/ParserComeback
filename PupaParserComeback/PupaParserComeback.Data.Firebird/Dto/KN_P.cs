namespace PupaParserComeback.Data.Firebird.Dto
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Firebird.KN_P")]
    public partial class KN_P
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [StringLength(30)]
        public string FAM { get; set; }

        [StringLength(30)]
        public string IM { get; set; }

        [StringLength(30)]
        public string OTCH { get; set; }

        [StringLength(15)]
        public string D_ROD { get; set; }

        [StringLength(30)]
        public string RVK { get; set; }

        [StringLength(255)]
        public string KUDA { get; set; }

        [StringLength(63)]
        public string KTO { get; set; }

        [StringLength(25)]
        public string IGNORE { get; set; }
    }
}
