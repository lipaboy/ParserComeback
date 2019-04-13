using System;

namespace PupaParserComeback.Domain.Indexes
{
    public class RecruitIndex : IEquatable<RecruitIndex>
    {
        public string Surname { get; }
        public string Name { get; }
        public string Patronymic { get; }
        public string RegionalCollectionPoint { get; }

        public RecruitIndex(string surname, 
            string name, 
            string patronymic, 
            string regionalCollectionPoint)
        {
            if (surname == null)
            {
                throw new ArgumentNullException(nameof(surname));
            }

            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (patronymic == null)
            {
                throw new ArgumentNullException(nameof(patronymic));
            }

            if (regionalCollectionPoint == null)
            {
                throw new ArgumentNullException(nameof(regionalCollectionPoint));
            }

            Surname = surname;
            Name = name;
            Patronymic = patronymic;
            RegionalCollectionPoint = regionalCollectionPoint;
        }

        #region Equals logic

        public bool Equals(RecruitIndex other)
        {
            if (other == null) return false;

            return Surname == other.Surname &&
                   Name == other.Name &&
                   Patronymic == other.Patronymic &&
                   RegionalCollectionPoint == other.RegionalCollectionPoint;
        }

        public override bool Equals(object obj)
        {
            var source = obj as RecruitIndex;
            if (source == null) return false;

            return Equals(source);
        }

        public override int GetHashCode()
        {
            return Surname.GetHashCode() ^
                   Name.GetHashCode() ^
                   Patronymic.GetHashCode() ^
                   RegionalCollectionPoint.GetHashCode();
        }

        public static bool operator ==(RecruitIndex left, RecruitIndex right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;

            return left.Equals(right);
        }

        public static bool operator !=(RecruitIndex left, RecruitIndex right)
        {
            return !(left == right);
        }

        #endregion

        public override string ToString()
        {
            return $"{Surname} {Name} {Patronymic} {RegionalCollectionPoint}";
        }
    }
}
