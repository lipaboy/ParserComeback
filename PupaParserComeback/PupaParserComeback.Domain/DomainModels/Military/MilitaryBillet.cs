using PupaParserComeback.Domain.DomainModels.Common;
using System;

namespace PupaParserComeback.Domain.DomainModels.Military
{
    public class MilitaryBillet : IEquatable<MilitaryBillet>
    {
        public Code BilletNumber { get; private set; }
        public SecretAccess SecretAccess { get; private set; }

        public bool IsHaveSecretAccess
        {
            get { return SecretAccess != null; }
        }

        public MilitaryBillet(Code billetNumber, SecretAccess secretAccess = null)
        {
            if (billetNumber == null)
            {
                throw new ArgumentNullException(nameof(billetNumber));
            }

            BilletNumber = billetNumber;
            SecretAccess = secretAccess;
        }

        #region Equals Logic

        public override bool Equals(object obj)
        {
            var source = obj as MilitaryBillet;
            if (source == null) return false;

            return Equals(source);
        }

        public override int GetHashCode()
        {
            return BilletNumber.GetHashCode() ^ SecretAccess.GetHashCode();
        }

        public bool Equals(MilitaryBillet other)
        {
            if (other == null) return false;

            return BilletNumber == other.BilletNumber &&
                   SecretAccess == other.SecretAccess;
        }

        public static bool operator ==(MilitaryBillet left, MilitaryBillet right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;

            return left.Equals(right);
        }

        public static bool operator !=(MilitaryBillet left, MilitaryBillet right)
        {
            return !(left == right);
        }

        #endregion
    }
}
