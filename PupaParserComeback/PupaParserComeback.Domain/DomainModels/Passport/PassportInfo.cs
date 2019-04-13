using PupaParserComeback.Domain.DomainModels.Common;
using System;

namespace PupaParserComeback.Domain.DomainModels.Passport
{
    public class PassportInfo : IEquatable<PassportInfo>
    {
        public string PhotoName { get; private set; }

        public Code Code { get; private set; }
        public PassportIssueInfo IssueInfo { get; private set; }
        public PersonInfo PersonInfo { get; private set; }
        public PassportLocationInfo LocationInfo { get; private set; }
        public PassportFamilyInfo FamilyInfo { get; private set; }

        public PassportInfo(string photoName,
            Code code,
            PassportIssueInfo issueInfo,
            PersonInfo personInfo,
            PassportLocationInfo locationInfo,
            PassportFamilyInfo familyInfo)
        {
            ChangePhotoName(photoName);

            if (code == null)
            {
                throw new ArgumentNullException(nameof(code));
            }

            if (issueInfo == null)
            {
                throw new ArgumentNullException(nameof(code));
            }

            if (personInfo == null)
            {
                throw new ArgumentNullException(nameof(code));
            }

            if (locationInfo == null)
            {
                throw new ArgumentNullException(nameof(code));
            }

            if (familyInfo == null)
            {
                throw new ArgumentNullException(nameof(code));
            }

            Code = code;
            IssueInfo = issueInfo;
            PersonInfo = personInfo;
            LocationInfo = locationInfo;
            FamilyInfo = familyInfo;
        }

        public void ChangePhotoName(string photoName)
        {
            PhotoName = photoName;
        }

        #region Equals Logic

        public override bool Equals(object obj)
        {
            var source = obj as PassportInfo;
            if (source == null) return false;

            return Equals(source);
        }

        public override int GetHashCode()
        {
            return PhotoName.GetHashCode() ^ Code.GetHashCode() ^
                   IssueInfo.GetHashCode() ^ PersonInfo.GetHashCode() ^
                   LocationInfo.GetHashCode() ^ FamilyInfo.GetHashCode();
        }

        public bool Equals(PassportInfo other)
        {
            if (other == null) return false;

            return PhotoName == other.PhotoName &&
                   Code == other.Code &&
                   IssueInfo == other.IssueInfo &&
                   PersonInfo == other.PersonInfo &&
                   LocationInfo == other.LocationInfo &&
                   FamilyInfo == other.FamilyInfo;
        }

        public static bool operator ==(PassportInfo left, PassportInfo right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;

            return left.Equals(right);
        }

        public static bool operator !=(PassportInfo left, PassportInfo right)
        {
            return !(left == right);
        }

        #endregion
    }
}
