using PupaParserComeback.Domain.Enums;

namespace PupaParserComeback.Domain.ExtensionMethods.EnumExtensions
{
    public static class BloodTypeExtensions
    {
        public static string ToBloodTypeString(this BloodType source)
        {
            switch (source)
            {
                case BloodType.OnePlus: return "1+";
                case BloodType.OneMinus: return "1-";
                case BloodType.TwoPlus: return "2+";
                case BloodType.TwoMinus: return "2-";
                case BloodType.ThreePlus: return "3+";
                case BloodType.ThreeMinus: return "3-";
                case BloodType.FourthPlus: return "4+";
                case BloodType.FourthMinus: return "4-";
            }

            return string.Empty;
        }

        public static BloodType ToBloodTypeEnum(this string source)
        {
            switch (source)
            {
                case "1+": return BloodType.OnePlus;
                case "1-": return BloodType.OneMinus;
                case "2+": return BloodType.TwoPlus;
                case "2-": return BloodType.TwoMinus;
                case "3+": return BloodType.ThreePlus;
                case "3-": return BloodType.ThreeMinus;
                case "4+": return BloodType.FourthPlus;
                case "4-": return BloodType.FourthMinus;
            }

            return BloodType.None;
        }
    }
}
