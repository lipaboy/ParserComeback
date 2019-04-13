using PupaParserComeback.Domain.Enums;
using System;

namespace PupaParserComeback.Domain.DomainModels.Civil
{
    public class CivilInfo : IEquatable<CivilInfo>
    {
        public EducationStatus Education { get; private set; }
        public string Profession { get; private set; }
        public OccupationStatus Occupation { get; private set; }

        public CivilInfo(EducationStatus education, string profession, OccupationStatus occupation)
        {
            Education = education;
            ChangeProfession(profession);
            Occupation = occupation;
        }

        public void ChangeEducation(EducationStatus education)
        {
            Education = education;
        }

        public void ChangeProfession(string profession)
        {
            Profession = profession;
        }

        public void ChangeOccupation(OccupationStatus occupation)
        {
            Occupation = occupation;
        }

        #region Equals Logic

        public override bool Equals(object obj)
        {
            var source = obj as CivilInfo;
            if (source == null) return false;

            return Equals(source);
        }

        public override int GetHashCode()
        {
            return Education.GetHashCode() ^ Profession.GetHashCode() ^ Occupation.GetHashCode();
        }

        public bool Equals(CivilInfo other)
        {
            if (other == null) return false;

            return Education == other.Education &&
                   Profession == other.Profession &&
                   Occupation == other.Occupation;
        }

        public static bool operator ==(CivilInfo left, CivilInfo right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;

            return left.Equals(right);
        }

        public static bool operator !=(CivilInfo left, CivilInfo right)
        {
            return !(left == right);
        }

        #endregion
    }
}
