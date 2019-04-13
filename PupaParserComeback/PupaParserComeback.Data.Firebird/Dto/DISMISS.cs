namespace PupaParserComeback.Data.Firebird.Dto
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Firebird.DISMISS")]
    public partial class DISMISS
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public int RECRUIT_ID { get; set; }

        [StringLength(255)]
        public string FIO_IN_GENITIVE { get; set; }

        [Required]
        [StringLength(50)]
        public string NO { get; set; }

        [Column(TypeName = "date")]
        public DateTime LEAVE_DATE { get; set; }

        [Column(TypeName = "date")]
        public DateTime ARRIVAL_DATE { get; set; }

        public TimeSpan ARRIVAL_TIME { get; set; }

        [Required]
        [StringLength(255)]
        public string DISMISS_ADDRES { get; set; }

        [StringLength(20)]
        public string TO_KOM { get; set; }

        public DateTime? TIMESTAMP { get; set; }
    }
}
