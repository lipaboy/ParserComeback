using System;

namespace PupaParserComeback.Domain.DomainModels.Common
{
    public class PersonInfo : IEquatable<PersonInfo>
    {
        public FullName FullName { get; private set; }

        public BirthInfo BirthInfo { get; private set; }

        public PersonInfo(FullName fullName, BirthInfo birthInfo)
        {
            if (fullName == null)
            {
                throw new ArgumentNullException(nameof(fullName));
            }

            if (birthInfo == null)
            {
                throw new ArgumentNullException(nameof(birthInfo));
            }
            
            FullName = fullName;
            BirthInfo = birthInfo;
        }

        #region Equals Logic

        public override bool Equals(object obj)
        {
            var source = obj as PersonInfo;
            if (source == null) return false;

            return Equals(source);
        }

        public override int GetHashCode()
        {
            return FullName.GetHashCode() ^ BirthInfo.GetHashCode();
        }

        public bool Equals(PersonInfo other)
        {
            if (other == null) return false;

            return FullName == other.FullName &&
                   BirthInfo == other.BirthInfo;
        }

        public static bool operator ==(PersonInfo left, PersonInfo right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;

            return left.Equals(right);
        }

        public static bool operator !=(PersonInfo left, PersonInfo right)
        {
            return !(left == right);
        }

        #endregion
    }
}
