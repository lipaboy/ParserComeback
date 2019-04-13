namespace PupaParserComeback.Data.Firebird.Dto
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Firebird.IBE$LOG_TABLES")]
    public partial class IBE_LOG_TABLES
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Required]
        [StringLength(31)]
        public string TABLE_NAME { get; set; }

        [Required]
        [StringLength(1)]
        public string OPERATION { get; set; }

        public DateTime DATE_TIME { get; set; }

        [Required]
        [StringLength(31)]
        public string USER_NAME { get; set; }
    }
}
