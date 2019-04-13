using PupaParserComeback.Domain.Enums;
using System;

namespace PupaParserComeback.Domain.DomainModels.Military
{
    public class ProficiencyCardInfo : IEquatable<ProficiencyCardInfo>
    {
        public ProficiencyCategory ProficiencyCategory { get; private set; }
        public OfficialStatus OfficialStatus { get; private set; }

        public NervouslyPsychologicalStatus NervouslyPsychologicalStability { get; private set; }
        public GeneralPsychologicalStatus GeneralPsychologicalStability { get; private set; }

        public ProficiencyCardInfo(ProficiencyCategory proficiencyCategory,
            OfficialStatus officialstatus,
            NervouslyPsychologicalStatus nervously,
            GeneralPsychologicalStatus general)
        {
            ChangeProficiencyCategory(proficiencyCategory);
            ChangeOfficialStatusChange(officialstatus);
            ChangeNervouslyPsychologicalStability(nervously);
            ChangeGeneralPsychologicalStability(general);
        }

        public void ChangeProficiencyCategory(ProficiencyCategory proficiencyCategory)
        {
            ProficiencyCategory = proficiencyCategory;
        }

        public void ChangeOfficialStatusChange(OfficialStatus officialstatus)
        {
            OfficialStatus = officialstatus;
        }

        public void ChangeNervouslyPsychologicalStability(NervouslyPsychologicalStatus nervously)
        {
            NervouslyPsychologicalStability = nervously;
        }

        public void ChangeGeneralPsychologicalStability(GeneralPsychologicalStatus general)
        {
            GeneralPsychologicalStability = general;
        }

        #region Equals Logic

        public override bool Equals(object obj)
        {
            var source = obj as MilitaryInfo;
            if (source == null) return false;

            return Equals(source);
        }

        public override int GetHashCode()
        {
            return ProficiencyCategory.GetHashCode() ^ OfficialStatus.GetHashCode() ^
                   GeneralPsychologicalStability.GetHashCode() ^ NervouslyPsychologicalStability.GetHashCode();
        }

        public bool Equals(ProficiencyCardInfo other)
        {
            if (other == null) return false;

            return ProficiencyCategory == other.ProficiencyCategory &&
                   OfficialStatus == other.OfficialStatus &&
                   GeneralPsychologicalStability == other.GeneralPsychologicalStability &&
                   NervouslyPsychologicalStability == other.NervouslyPsychologicalStability;
        }

        public static bool operator ==(ProficiencyCardInfo left, ProficiencyCardInfo right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;

            return left.Equals(right);
        }

        public static bool operator !=(ProficiencyCardInfo left, ProficiencyCardInfo right)
        {
            return !(left == right);
        }

        #endregion
    }
}
