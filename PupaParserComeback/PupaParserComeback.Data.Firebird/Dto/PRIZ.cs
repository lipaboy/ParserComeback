namespace PupaParserComeback.Data.Firebird.Dto
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Firebird.PRIZ")]
    public partial class PRIZ
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

        [StringLength(25)]
        public string N_KOM { get; set; }

        public int? FL_UB { get; set; }

        [StringLength(25)]
        public string SPEC { get; set; }

        [StringLength(26)]
        public string BRAK { get; set; }

        [StringLength(26)]
        public string SUD { get; set; }

        [StringLength(30)]
        public string OBRAZOV { get; set; }

        [StringLength(30)]
        public string PROF_P { get; set; }

        [StringLength(30)]
        public string NPU { get; set; }

        [StringLength(30)]
        public string REZH_KOM { get; set; }

        [StringLength(1)]
        public string GODN { get; set; }

        public int? P_PREDN { get; set; }

        [StringLength(30)]
        public string TDT { get; set; }

        public int? F_DOP { get; set; }

        [StringLength(25)]
        public string N_DOP { get; set; }

        [Column(TypeName = "date")]
        public DateTime? D_DOP { get; set; }

        [Column(TypeName = "date")]
        public DateTime? D_PRIB { get; set; }

        public int? FL_UV { get; set; }

        [Column(TypeName = "date")]
        public DateTime? D_U_UVOL { get; set; }

        [Column(TypeName = "date")]
        public DateTime? D_P_UVOL { get; set; }

        public int? FL_SOCH { get; set; }

        [Column(TypeName = "date")]
        public DateTime? D_U_SOCH { get; set; }

        [StringLength(25)]
        public string HIST { get; set; }

        public int? ROST { get; set; }

        public int? MASSA { get; set; }

        [StringLength(30)]
        public string STAT { get; set; }

        [StringLength(25)]
        public string S_V_BIL { get; set; }

        [StringLength(26)]
        public string N_V_BIL { get; set; }

        public string PRIM { get; set; }

        [StringLength(30)]
        public string PR_UBIT { get; set; }

        [StringLength(30)]
        public string PRIM_UBIT { get; set; }

        [StringLength(26)]
        public string ZREN { get; set; }

        [StringLength(25)]
        public string R_G_U { get; set; }

        [StringLength(25)]
        public string R_O_G { get; set; }

        [StringLength(25)]
        public string R_OB { get; set; }

        [StringLength(26)]
        public string H { get; set; }

        public int? POSTO { get; set; }

        [StringLength(25)]
        public string LN_SER { get; set; }

        [StringLength(26)]
        public string LN_NUM { get; set; }

        [StringLength(30)]
        public string S_PASPORT { get; set; }

        [StringLength(30)]
        public string N_PASPORT { get; set; }

        [Column(TypeName = "date")]
        public DateTime? D_PASPORT { get; set; }

        public int? IMEET_RAZR { get; set; }

        public int? IMEET_REB { get; set; }

        public int? ODIN_ROD { get; set; }

        public int? BEZ_ROD { get; set; }

        [StringLength(63)]
        public string DO_PRIZ { get; set; }

        [StringLength(30)]
        public string NA_UCHETE { get; set; }

        public short? NAVY { get; set; }

        [StringLength(4)]
        public string S_VA { get; set; }

        [StringLength(6)]
        public string N_VA { get; set; }

        [StringLength(80)]
        public string M_ROD { get; set; }

        [StringLength(120)]
        public string KEM_VIDAN { get; set; }

        public int? TSP { get; set; }
    }
}
