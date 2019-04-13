using PupaParserComeback.Domain.DomainModels.Common;
using System;

namespace PupaParserComeback.Domain.DomainModels.Military
{
    public class MilitaryInfo : IEquatable<MilitaryInfo>
    {
        public const string NoSpeciality = "ОСТ";
        public const string NoTeamMode = "НЕРЕЖ.";

        public Code PersonalNumber { get; private set; }
        public MilitaryBillet Billet { get; private set; }
        public ProficiencyCardInfo ProficiencyCard { get; private set; }

        public string Speciality { get; private set; }
        public string TeamMode { get; private set; }

        public MilitaryInfo(Code personalNumber,
            MilitaryBillet billet,
            ProficiencyCardInfo proficiencyCard,
            string speciality,
            string teamMode)
        {
            if (personalNumber == null)
            {
                throw new ArgumentNullException(nameof(personalNumber));
            }

            if (billet == null)
            {
                throw new ArgumentNullException(nameof(billet));
            }

            if (proficiencyCard == null)
            {
                throw new ArgumentNullException(nameof(proficiencyCard));
            }

            PersonalNumber = personalNumber;
            Billet = billet;
            ProficiencyCard = proficiencyCard;

            ChangeSpeciality(speciality);
            ChangeTeamMode(teamMode);
        }

        public void ChangeSpeciality(string speciality)
        {
            if (string.IsNullOrWhiteSpace(speciality))
            {
                throw new ArgumentNullException(nameof(speciality));
            }

            Speciality = speciality;
        }

        public void ChangeTeamMode(string teamMode)
        {
            if (string.IsNullOrWhiteSpace(teamMode))
            {
                throw new ArgumentNullException(nameof(teamMode));
            }

            TeamMode = teamMode;
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
            return PersonalNumber.GetHashCode() ^ Billet.GetHashCode() ^
                   ProficiencyCard.GetHashCode() ^ Speciality.GetHashCode() ^ 
                   TeamMode.GetHashCode();
        }

        public bool Equals(MilitaryInfo other)
        {
            if (other == null) return false;

            return PersonalNumber == other.PersonalNumber &&
                   Billet == other.Billet &&
                   ProficiencyCard == other.ProficiencyCard &&
                   Speciality == other.Speciality &&
                   TeamMode == other.TeamMode;
        }

        public static bool operator ==(MilitaryInfo left, MilitaryInfo right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;

            return left.Equals(right);
        }

        public static bool operator !=(MilitaryInfo left, MilitaryInfo right)
        {
            return !(left == right);
        }

        #endregion
    }
}
