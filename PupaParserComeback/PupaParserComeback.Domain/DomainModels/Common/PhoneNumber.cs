using System;

namespace PupaParserComeback.Domain.DomainModels.Common
{
    public class PhoneNumber : IEquatable<PhoneNumber>
    {
        public string Value { get; private set; }

        public PhoneNumber(string value)
        {
            ChangePhoneNumber(value);
        }

        public void ChangePhoneNumber(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            Value = value;
        }

        #region Equals Logic

        public override bool Equals(object obj)
        {
            var source = obj as PhoneNumber;
            if (source == null) return false;

            return Equals(source);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public bool Equals(PhoneNumber other)
        {
            if (other == null) return false;

            return Value == other.Value;
        }

        public static bool operator ==(PhoneNumber left, PhoneNumber right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;

            return left.Equals(right);
        }

        public static bool operator !=(PhoneNumber left, PhoneNumber right)
        {
            return !(left == right);
        }

        #endregion
    }
}
