namespace PupaParserComeback.Data.Firebird.Dto
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Firebird.kom")]
    public partial class kom
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [StringLength(25)]
        public string N_KOM { get; set; }

        [StringLength(26)]
        public string V_VS { get; set; }

        [StringLength(30)]
        public string N_E { get; set; }

        [StringLength(30)]
        public string ST { get; set; }

        [StringLength(30)]
        public string V_CH { get; set; }

        [StringLength(30)]
        public string REZH_KOM { get; set; }

        [Column(TypeName = "date")]
        public DateTime? D_OTPR { get; set; }

        [StringLength(30)]
        public string V_SHET { get; set; }

        public int? VSEGO { get; set; }

        public int? VA { get; set; }

        public int? MTLB { get; set; }

        public int? SUD { get; set; }

        [StringLength(30)]
        public string Z_PREDS { get; set; }

        [StringLength(30)]
        public string PREDS { get; set; }

        [StringLength(25)]
        public string S_UD { get; set; }

        [StringLength(26)]
        public string N_UD { get; set; }

        [StringLength(30)]
        public string VIDAN { get; set; }

        [Column(TypeName = "date")]
        public DateTime? D_VID { get; set; }

        public string PRIM { get; set; }

        [StringLength(26)]
        public string SPEC { get; set; }

        [StringLength(25)]
        public string VUS1 { get; set; }

        [StringLength(25)]
        public string VUS2 { get; set; }

        [StringLength(25)]
        public string VUS3 { get; set; }

        public int? PR { get; set; }

        [Column(TypeName = "date")]
        public DateTime? D_PR { get; set; }

        [StringLength(30)]
        public string RAILROAD { get; set; }

        [StringLength(30)]
        public string V_OKRUG { get; set; }

        public int? FL_UB { get; set; }

        public int? FL_PEREOD { get; set; }

        public int? NARAD { get; set; }

        [StringLength(100)]
        public string GOROD { get; set; }

        [StringLength(255)]
        public string STREET { get; set; }

        [StringLength(30)]
        public string PHONE { get; set; }

        [StringLength(10)]
        public string ZIP { get; set; }

        public int? ISSIBVO { get; set; }

        [StringLength(255)]
        public string DOLJNOST { get; set; }

        public short? FOLLOW_OFICER { get; set; }

        public short? FOLLOW_PRAPOR { get; set; }

        public short? FOLLOW_SOLDERS { get; set; }

        [StringLength(26)]
        public string NVESHAT { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DVESHAT { get; set; }

        public short? KOLSUHPAY { get; set; }

        public short SYST { get; set; }

        [StringLength(6)]
        public string POEZD { get; set; }

        [StringLength(12)]
        public string VID_TR { get; set; }

        [StringLength(6)]
        public string T_OTPR { get; set; }

        [StringLength(20)]
        public string N_DOV { get; set; }

        [Column(TypeName = "date")]
        public DateTime? D_DOV { get; set; }

        public int FL_DISPLAY { get; set; }

        public int? DOPUSK { get; set; }

        public int? AKPM { get; set; }
    }
}
