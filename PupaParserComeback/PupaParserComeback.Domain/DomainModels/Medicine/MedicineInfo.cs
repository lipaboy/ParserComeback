using System;

namespace PupaParserComeback.Domain.DomainModels.Medicine
{
    public class MedicineInfo : IEquatable<MedicineInfo>
    {
        public Health Health { get; private set; }
        public PhysiologicalCharacteristics PhysiologicalCharacteristics { get; private set; }
        public SportInfo SportInfo { get; private set; }

        public bool IsSportsman
        {
            get { return SportInfo.Rank != Enums.SportRank.HaveNot; }
        }

        public MedicineInfo(Health health, PhysiologicalCharacteristics physiologicalCharacteristics, 
            SportInfo sportInfo)
        {
            if (health == null)
            {
                throw new ArgumentNullException(nameof(health));
            }

            if (physiologicalCharacteristics == null)
            {
                throw new ArgumentNullException(nameof(physiologicalCharacteristics));
            }

            Health = health;
            PhysiologicalCharacteristics = physiologicalCharacteristics;
            SportInfo = sportInfo ?? new SportInfo();
        }

        #region Equals Logic

        public override bool Equals(object obj)
        {
            var source = obj as Health;
            if (source == null) return false;

            return Equals(source);
        }

        public override int GetHashCode()
        {
            return Health.GetHashCode() ^ 
                   PhysiologicalCharacteristics.GetHashCode() ^
                   SportInfo.GetHashCode();
        }

        public bool Equals(MedicineInfo other)
        {
            if (other == null) return false;

            return Health == other.Health &&
                   PhysiologicalCharacteristics == other.PhysiologicalCharacteristics &&
                   SportInfo == other.SportInfo;
        }

        public static bool operator ==(MedicineInfo left, MedicineInfo right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;

            return left.Equals(right);
        }

        public static bool operator !=(MedicineInfo left, MedicineInfo right)
        {
            return !(left == right);
        }

        #endregion
    }
}
