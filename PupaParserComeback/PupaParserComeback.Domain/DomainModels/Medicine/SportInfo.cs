using PupaParserComeback.Domain.Enums;
using System;

namespace PupaParserComeback.Domain.DomainModels.Medicine
{
    public class SportInfo : IEquatable<SportInfo>
    {
        public SportRank Rank { get; private set; }
        public string Kind { get; private set; }

        public SportInfo(SportRank rank = SportRank.HaveNot, string kind = "")
        {
            Rank = rank;
            Kind = kind;
        }

        #region Equals Logic

        public override bool Equals(object obj)
        {
            var source = obj as PhysiologicalCharacteristics;
            if (source == null) return false;

            return Equals(source);
        }

        public override int GetHashCode()
        {
            return Rank.GetHashCode() ^ Kind.GetHashCode();
        }

        public bool Equals(SportInfo other)
        {
            if (other == null) return false;

            return Rank == other.Rank && 
                   Kind == other.Kind;
        }

        public static bool operator ==(SportInfo left, SportInfo right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;

            return left.Equals(right);
        }

        public static bool operator !=(SportInfo left, SportInfo right)
        {
            return !(left == right);
        }

        #endregion
    }
}
