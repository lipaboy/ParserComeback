using PupaParserComeback.Domain.DomainModels.Common;
using System;

namespace PupaParserComeback.Domain.DomainModels.Passport
{
    public class PassportLocationInfo : IEquatable<PassportLocationInfo>
    {
        public Address RegisterLocation { get; private set; }
        public Address ActuallyLocation { get; private set; }

        public string Locality { get; private set; }

        public PassportLocationInfo(Address registerLocation, Address actuallyLocation, string locality = null)
        {
            if (registerLocation == null)
            {
                throw new ArgumentNullException(nameof(registerLocation));
            }

            if (actuallyLocation == null)
            {
                throw new ArgumentNullException(nameof(actuallyLocation));
            }

            RegisterLocation = registerLocation;
            ActuallyLocation = actuallyLocation;
            
            ChangeLocality(locality);
        }

        public void ChangeLocality(string locality)
        {
            Locality = locality;
        }

        #region Equals Logic

        public override bool Equals(object obj)
        {
            var source = obj as PassportLocationInfo;
            if (source == null) return false;

            return Equals(source);
        }

        public override int GetHashCode()
        {
            return RegisterLocation.GetHashCode() ^ ActuallyLocation.GetHashCode();
        }

        public bool Equals(PassportLocationInfo other)
        {
            if (other == null) return false;

            return RegisterLocation == other.RegisterLocation &&
                   ActuallyLocation == other.ActuallyLocation;
        }

        public static bool operator ==(PassportLocationInfo left, PassportLocationInfo right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;

            return left.Equals(right);
        }

        public static bool operator !=(PassportLocationInfo left, PassportLocationInfo right)
        {
            return !(left == right);
        }

        #endregion
    }
}
