using PupaParserComeback.Domain.Enums;
using System;

namespace PupaParserComeback.Domain.DomainModels.Common
{
    public class RelativeInfo : IEquatable<RelativeInfo>
    {
        public const string NotWorking = "Не работает";

        public RelativeStatus RelativeStatus { get; private set; }
        public PersonInfo PersonInfo { get; private set; }
        public string WorkPlace { get; private set; }
        
        public RelativeInfo(RelativeStatus relativeStatus, PersonInfo personInfo, string workPlace)
        {
            ChangeRelativeStatus(relativeStatus);

            if (personInfo == null)
            {
                throw new ArgumentNullException(nameof(personInfo));
            }

            PersonInfo = personInfo;

            ChangeWorkPlace(workPlace);
        }

        public void ChangeRelativeStatus(RelativeStatus relativeStatus)
        {
            RelativeStatus = relativeStatus;
        }

        public void ChangeWorkPlace(string workPlace)
        {
            if (workPlace == null)
            {
                throw new ArgumentNullException(nameof(workPlace));
            }

            WorkPlace = workPlace;
        }

        #region Equals Logic

        public override bool Equals(object obj)
        {
            var source = obj as RelativeInfo;
            if (source == null) return false;

            return Equals(source);
        }

        public override int GetHashCode()
        {
            return RelativeStatus.GetHashCode() ^ PersonInfo.GetHashCode() ^ WorkPlace.GetHashCode();
        }

        public bool Equals(RelativeInfo other)
        {
            if (other == null) return false;

            return RelativeStatus == other.RelativeStatus &&
                   PersonInfo == other.PersonInfo &&
                   WorkPlace == other.WorkPlace;
        }

        public static bool operator ==(RelativeInfo left, RelativeInfo right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;

            return left.Equals(right);
        }

        public static bool operator !=(RelativeInfo left, RelativeInfo right)
        {
            return !(left == right);
        }

        #endregion
    }
}
