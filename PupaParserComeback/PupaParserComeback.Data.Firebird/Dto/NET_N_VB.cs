namespace PupaParserComeback.Data.Firebird.Dto
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Firebird.NET_N_VB")]
    public partial class NET_N_VB
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public int? KOD { get; set; }

        [Column(TypeName = "date")]
        public DateTime? D_LOG { get; set; }
    }
}
