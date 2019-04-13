namespace PupaParserComeback.Data.Firebird.Dto
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Firebird.DepartureKom")]
    public partial class DepartureKom
    {
        [Key]
        [StringLength(7)]
        public string n_kom { get; set; }

        [Column(TypeName = "date")]
        public DateTime? OSPDate { get; set; }

        public TimeSpan? OSPTime { get; set; }

        [StringLength(16)]
        public string Train1Number { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Train1DateFrom { get; set; }

        public TimeSpan? Train1TimeFrom { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Train1DateTo { get; set; }

        public TimeSpan? Train1TimeTo { get; set; }

        [StringLength(16)]
        public string Train2Number { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Train2DateFrom { get; set; }

        public TimeSpan? Train2TimeFrom { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Train2DateTo { get; set; }

        public TimeSpan? Train2TimeTo { get; set; }

        [StringLength(32)]
        public string OSPTransport { get; set; }
    }
}
