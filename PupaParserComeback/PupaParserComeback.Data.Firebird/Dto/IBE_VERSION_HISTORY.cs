namespace PupaParserComeback.Data.Firebird.Dto
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Firebird.IBE$VERSION_HISTORY")]
    public partial class IBE_VERSION_HISTORY
    {
        [Key]
        [Column("IBE$VH_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IBE_VH_ID { get; set; }

        [Column("IBE$VH_MODIFY_DATE")]
        public DateTime IBE_VH_MODIFY_DATE { get; set; }

        [Column("IBE$VH_USER_NAME")]
        [StringLength(31)]
        public string IBE_VH_USER_NAME { get; set; }

        [Column("IBE$VH_OBJECT_TYPE")]
        public short IBE_VH_OBJECT_TYPE { get; set; }

        [Column("IBE$VH_OBJECT_NAME")]
        [Required]
        [StringLength(31)]
        public string IBE_VH_OBJECT_NAME { get; set; }

        [Column("IBE$VH_HEADER", TypeName = "varchar")]
        [StringLength(32000)]
        public string IBE_VH_HEADER { get; set; }

        [Column("IBE$VH_BODY")]
        public byte[] IBE_VH_BODY { get; set; }

        [Column("IBE$VH_DESCRIPTION")]
        public string IBE_VH_DESCRIPTION { get; set; }
    }
}
