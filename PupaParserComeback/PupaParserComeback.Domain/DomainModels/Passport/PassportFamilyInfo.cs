using PupaParserComeback.Domain.Enums;
using System;

namespace PupaParserComeback.Domain.DomainModels.Passport
{
    public class PassportFamilyInfo : IEquatable<PassportFamilyInfo>
    {
        public const string HaveBaby = "Имеет";
        public const string NotHaveBaby = "Не имеет";

        public FamilyStatus FamilyStatus { get; private set; }
        public bool IsHaveBaby { get; private set; }

        public PassportFamilyInfo(FamilyStatus familyStatus, bool isHaveBaby)
        {
            ChangeFamilyStatus(familyStatus);
            ChangeIsHaveBaby(isHaveBaby);
        }

        public void ChangeFamilyStatus(FamilyStatus familyStatus)
        {
            FamilyStatus = familyStatus;
        }

        public void ChangeIsHaveBaby(bool isHaveBaby)
        {
            IsHaveBaby = isHaveBaby;
        }

        #region Equals Logic

        public override bool Equals(object obj)
        {
            var source = obj as PassportFamilyInfo;
            if (source == null) return false;

            return Equals(source);
        }

        public override int GetHashCode()
        {
            return FamilyStatus.GetHashCode() ^ IsHaveBaby.GetHashCode();
        }

        public bool Equals(PassportFamilyInfo other)
        {
            if (other == null) return false;

            return FamilyStatus == other.FamilyStatus &&
                   IsHaveBaby == other.IsHaveBaby;
        }

        public static bool operator ==(PassportFamilyInfo left, PassportFamilyInfo right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;

            return left.Equals(right);
        }

        public static bool operator !=(PassportFamilyInfo left, PassportFamilyInfo right)
        {
            return !(left == right);
        }

        #endregion
    }
}
