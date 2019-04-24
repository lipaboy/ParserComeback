using System;

namespace PupaParserComeback.Data.Firebird.Dto
{
    public partial class PRIZ : IEquatable<PRIZ>
    {
        public override bool Equals(object obj)
        {
            var source = obj as PRIZ;
            if (source == null) return false;

            return Equals(source);
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode() ^
                   RVK.GetHashCode() ^
                   D_PRIB.GetHashCode() ^

                   NA_UCHETE.GetHashCode() ^
                   SUD.GetHashCode() ^

                   GODN.GetHashCode() ^
                   P_PREDN.GetHashCode() ^
                   TDT.GetHashCode() ^
                   STAT.GetHashCode() ^
                   ZREN.GetHashCode() ^
                   ROST.GetHashCode() ^
                   MASSA.GetHashCode() ^
                   R_G_U.GetHashCode() ^
                   R_O_G.GetHashCode() ^
                   R_OB.GetHashCode() ^
                   IMEET_RAZR.GetHashCode() ^

                   KEM_VIDAN.GetHashCode() ^
                   D_PASPORT.GetHashCode() ^
                   FAM.GetHashCode() ^
                   IM.GetHashCode() ^
                   OTCH.GetHashCode() ^
                   D_ROD.GetHashCode() ^
                   M_ROD.GetHashCode() ^
                   S_PASPORT.GetHashCode() ^
                   N_PASPORT.GetHashCode() ^
                   BRAK.GetHashCode() ^
                   IMEET_REB.GetHashCode() ^

                   LN_SER.GetHashCode() ^
                   LN_NUM.GetHashCode() ^
                   S_V_BIL.GetHashCode() ^
                   N_V_BIL.GetHashCode() ^
                   F_DOP.GetHashCode() ^
                   N_DOP.GetHashCode() ^
                   D_DOP.GetHashCode() ^
                   PROF_P.GetHashCode() ^
                   NPU.GetHashCode() ^
                   OPS.GetHashCode() ^
                   REZH_KOM.GetHashCode() ^

                   OBRAZOV.GetHashCode() ^
                   DO_PRIZ.GetHashCode() ^

                   ODIN_ROD.GetHashCode() ^
                   BEZ_ROD.GetHashCode() ^

                   S_VA.GetHashCode() ^
                   N_VA.GetHashCode() ^
                   SPEC.GetHashCode();
        }

        public bool Equals(PRIZ other)
        {
            return ID == other.ID &&
                   RVK == other.RVK &&
                   D_PRIB == other.D_PRIB &&

                   NA_UCHETE == other.NA_UCHETE &&
                   SUD == other.SUD &&

                   GODN == other.GODN &&
                   P_PREDN == other.P_PREDN &&
                   TDT == other.TDT &&
                   STAT == other.STAT &&
                   ZREN == other.ZREN &&
                   ROST == other.ROST &&
                   MASSA == other.MASSA &&
                   R_G_U == other.R_G_U &&
                   R_O_G == other.R_O_G &&
                   R_OB == other.R_OB &&
                   IMEET_RAZR == other.IMEET_RAZR &&

                   KEM_VIDAN == other.KEM_VIDAN &&
                   D_PASPORT == other.D_PASPORT &&
                   FAM == other.FAM &&
                   IM == other.IM &&
                   OTCH == other.OTCH &&
                   D_ROD == other.D_ROD &&
                   M_ROD == other.M_ROD &&
                   S_PASPORT == other.S_PASPORT &&
                   N_PASPORT == other.N_PASPORT &&
                   BRAK == other.BRAK &&
                   IMEET_REB == other.IMEET_REB &&

                   LN_SER == other.LN_SER &&
                   LN_NUM == other.LN_NUM &&
                   S_V_BIL == other.S_V_BIL &&
                   N_V_BIL == other.N_V_BIL &&
                   F_DOP == other.F_DOP &&
                   N_DOP == other.N_DOP &&
                   D_DOP == other.D_DOP &&
                   PROF_P == other.PROF_P &&
                   NPU == other.NPU &&
                   OPS == other.OPS &&
                   REZH_KOM == other.REZH_KOM &&

                   OBRAZOV == other.OBRAZOV &&
                   DO_PRIZ == other.DO_PRIZ &&

                   ODIN_ROD == other.ODIN_ROD &&
                   BEZ_ROD == other.BEZ_ROD &&

                   S_VA == other.S_VA &&
                   N_VA == other.N_VA &&
                   SPEC == other.SPEC;
        }
    }
}
