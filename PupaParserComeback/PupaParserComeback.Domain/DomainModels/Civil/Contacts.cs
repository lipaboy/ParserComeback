using PupaParserComeback.Domain.DomainModels.Common;
using System;

namespace PupaParserComeback.Domain.DomainModels.Civil
{
    public class Contacts : IEquatable<Contacts>
    {
        public PhoneNumber MobileNumber { get; private set; }
        public PhoneNumber HomeNumber { get; private set; }

        public Contacts(PhoneNumber mobile, PhoneNumber home)
        {
            if (mobile == null)
            {
                throw new ArgumentNullException(nameof(mobile));
            }

            if (home == null)
            {
                throw new ArgumentNullException(nameof(home));
            }

            MobileNumber = mobile;
            HomeNumber = home;
        }

        public void ChangeMobileNumber(PhoneNumber number)
        {
            if (number == null)
            {
                throw new ArgumentNullException(nameof(number));
            }

            MobileNumber = number;
        }

        public void ChangeHomeNumber(PhoneNumber number)
        {
            if (number == null)
            {
                throw new ArgumentNullException(nameof(number));
            }

            HomeNumber = number;
        }

        #region Equals Logic

        public override bool Equals(object obj)
        {
            var source = obj as Contacts;
            if (source == null) return false;

            return Equals(source);
        }

        public override int GetHashCode()
        {
            return MobileNumber.GetHashCode() ^ HomeNumber.GetHashCode();
        }

        public bool Equals(Contacts other)
        {
            if (other == null) return false;

            return MobileNumber == other.MobileNumber &&
                   HomeNumber == other.HomeNumber;
        }

        public static bool operator ==(Contacts left, Contacts right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;

            return left.Equals(right);
        }

        public static bool operator !=(Contacts left, Contacts right)
        {
            return !(left == right);
        }

        #endregion
    }
}
