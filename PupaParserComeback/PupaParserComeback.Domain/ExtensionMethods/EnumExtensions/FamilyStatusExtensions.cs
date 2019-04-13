using PupaParserComeback.Domain.Enums;

namespace PupaParserComeback.Domain.ExtensionMethods.EnumExtensions
{
    public static class FamilyStatusExtensions
    {
        public static string ToFamilyStatusString(this FamilyStatus source)
        {
            switch (source)
            {
                case FamilyStatus.Single: return "Холост";
                case FamilyStatus.Merried: return "Женат";
                case FamilyStatus.Divorced: return "Разведен";
                case FamilyStatus.Widower: return "Вдовец";
            }

            return string.Empty;
        }

        public static FamilyStatus ToFamilyStatusEnum(this string source)
        {
            switch (source)
            {
                case "Холост": return FamilyStatus.Single;
                case "Женат": return FamilyStatus.Merried;
                case "Разведен": return FamilyStatus.Divorced;
                case "Вдовец": return FamilyStatus.Widower;
            }

            return FamilyStatus.None;
        }
    }
}
