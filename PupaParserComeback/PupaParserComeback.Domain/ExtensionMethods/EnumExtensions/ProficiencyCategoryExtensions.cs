using PupaParserComeback.Domain.Enums;

namespace PupaParserComeback.Domain.ExtensionMethods.EnumExtensions
{
    public static class ProficiencyCategoryExtensions
    {
        public static string ToProficiencyCategoryString(this ProficiencyCategory source)
        {
            switch (source)
            {
                case ProficiencyCategory.First: return "I";
                case ProficiencyCategory.Second: return "II";
                case ProficiencyCategory.Third: return "III";
                case ProficiencyCategory.Fourth: return "IV";
            }

            return string.Empty;
        }

        public static ProficiencyCategory ToProficiencyCategoryEnum(this string source)
        {
            switch (source)
            {
                case "I": return ProficiencyCategory.First;
                case "II": return ProficiencyCategory.Second;
                case "III": return ProficiencyCategory.Third;
                case "IV": return ProficiencyCategory.Fourth;
            }

            return ProficiencyCategory.None;
        }
    }
}
