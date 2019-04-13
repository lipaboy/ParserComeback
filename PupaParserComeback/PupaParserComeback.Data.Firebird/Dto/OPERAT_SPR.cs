namespace PupaParserComeback.Data.Firebird.Dto
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Firebird.OPERAT_SPR")]
    public partial class OPERAT_SPR
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public int? DOSTAVLENO { get; set; }

        public int? VOZVRASHENO { get; set; }

        public int? VOZ_NGM { get; set; }

        public int? VOZ_DOPOBSL { get; set; }

        public int? VOZ_PNZ { get; set; }

        public int? VOZ_VUD { get; set; }

        public int? VOZ_SEM_OBS { get; set; }

        public int? SOCH { get; set; }

        public int? UVOLN { get; set; }

        public int? NA_OSP { get; set; }

        public int? DOST_V_POSL_SUT { get; set; }

        public int? OTPR_V_POSL_SUT { get; set; }

        public int? OTPR_VSEGO { get; set; }

        public int? O_VVS { get; set; }

        public int? O_VDV { get; set; }

        public int? O_VMF { get; set; }

        public int? O_KV { get; set; }

        public int? O_12_GU_MO { get; set; }

        public int? O_SIR { get; set; }

        public int? O_MCHS { get; set; }

        public int? O_ZHDV { get; set; }

        public int? O_PV { get; set; }

        public int? O_PP { get; set; }

        public int? O_VNG { get; set; }

        public int? O_RVSN { get; set; }

        public int? O_SV { get; set; }

        public int? O_SV_SIBVO { get; set; }

        public int? O_SSSI { get; set; }

        public int? O_CP { get; set; }

        public int? O_CHSS { get; set; }

        [Column(TypeName = "date")]
        public DateTime? D_SPRAV { get; set; }

        public TimeSpan? T_SPRAV { get; set; }

        public int? NA_OSP_SUD { get; set; }

        public int? OTPR_SUD { get; set; }

        public int? SOCH_SUD { get; set; }
    }
}
