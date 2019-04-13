namespace PupaParserComeback.Data.Firebird.Dto
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Firebird.DUP")]
    public partial class DUP
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
        public DateTime? DATE { get; set; }

        [StringLength(30)]
        public string LN_SER { get; set; }

        [StringLength(30)]
        public string LN_NUM { get; set; }
    }
}
