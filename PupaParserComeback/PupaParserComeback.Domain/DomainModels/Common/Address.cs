using System;

namespace PupaParserComeback.Domain.DomainModels.Common
{
    public class Address : IEquatable<Address>
    {
        public string Value { get; private set; }

        public Address(string value)
        {
            ChangeAddress(value);
        }

        public void ChangeAddress(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            Value = value;
        }

        #region Equals Logic

        public override bool Equals(object obj)
        {
            var source = obj as Address;
            if (source == null) return false;

            return Equals(source);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public bool Equals(Address other)
        {
            if (other == null) return false;

            return Value == other.Value;
        }

        public static bool operator ==(Address left, Address right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;

            return left.Equals(right);
        }

        public static bool operator !=(Address left, Address right)
        {
            return !(left == right);
        }

        #endregion
    }
}
