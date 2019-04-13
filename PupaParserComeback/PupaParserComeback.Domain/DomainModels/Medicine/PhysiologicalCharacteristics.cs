using System;

namespace PupaParserComeback.Domain.DomainModels.Medicine
{
    public class PhysiologicalCharacteristics : IEquatable<PhysiologicalCharacteristics>
    {
        public int? Height { get; private set; }
        public int? Weight { get; private set; }
        public int? HeadSize { get; private set; }
        public string ClothingSize { get; private set; }
        public int? ShoesSize { get; private set; }

        public PhysiologicalCharacteristics(int? height, 
            int? weight, 
            int? headSize,
            string clothingSize, 
            int? shoesSize)
        {
            ChangeHeight(height);
            ChangeWeight(weight);
            ChangeHeadSize(headSize);
            ChangeClothingSize(clothingSize);
            ChangeShoesSize(shoesSize);
        }

        public void ChangeHeight(int? height)
        {
            Height = height;
        }

        public void ChangeWeight(int? weight)
        {
            Weight = weight;
        }

        public void ChangeHeadSize(int? headSize)
        {
            HeadSize = headSize;
        }

        public void ChangeClothingSize(string clothingSize)
        {
            ClothingSize = clothingSize;
        }

        public void ChangeShoesSize(int? shoesSize)
        {
            ShoesSize = shoesSize;
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
            return Height.GetHashCode() ^
                   Weight.GetHashCode() ^
                   HeadSize.GetHashCode() ^
                   ClothingSize.GetHashCode() ^
                   ShoesSize.GetHashCode();
        }

        public bool Equals(PhysiologicalCharacteristics other)
        {
            if (other == null) return false;

            return Height == other.Height &&
                   Weight == other.Weight &&
                   HeadSize == other.HeadSize &&
                   ClothingSize == other.ClothingSize &&
                   ShoesSize == other.ShoesSize;
        }

        public static bool operator ==(PhysiologicalCharacteristics left, PhysiologicalCharacteristics right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;

            return left.Equals(right);
        }

        public static bool operator !=(PhysiologicalCharacteristics left, PhysiologicalCharacteristics right)
        {
            return !(left == right);
        }

        #endregion
    }
}
