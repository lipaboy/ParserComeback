using System;

namespace PupaParserComeback.Domain.DomainModels.Common
{
    public class BirthInfo : IEquatable<BirthInfo>
    {
        public const string UnknownPlace = "Не известно";

        public DateTime Date { get; private set; }
        public string Place { get; private set; }

        public BirthInfo(DateTime date, string place)
        {
            ChangeDate(date);
            ChangePlace(place);
        }

        public void ChangeDate(DateTime date)
        {
            Date = date;
        }

        public void ChangePlace(string place)
        {
            if (place == null)
            {
                throw new ArgumentNullException(nameof(place));
            }

            Place = place;
        }

        #region Equals Logic

        public override bool Equals(object obj)
        {
            var source = obj as BirthInfo;
            if (source == null) return false;

            return Equals(source);
        }

        public override int GetHashCode()
        {
            return Date.GetHashCode() ^ Place.GetHashCode();
        }

        public bool Equals(BirthInfo other)
        {
            if (other == null) return false;

            return Date == other.Date &&
                   Place == other.Place;
        }

        public static bool operator ==(BirthInfo left, BirthInfo right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;

            return left.Equals(right);
        }

        public static bool operator !=(BirthInfo left, BirthInfo right)
        {
            return !(left == right);
        }

        #endregion
    }
}
