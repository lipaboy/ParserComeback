using PupaParserComeback.Domain.Enums;

namespace PupaParserComeback.Domain.ExtensionMethods.EnumExtensions
{
    public static class ParentFamilyStatusExtensions
    {
        public static string ToParentFamilyStatusString(this ParentFamilyStatus source)
        {
            switch (source)
            {
                case ParentFamilyStatus.Full: return "Полная";
                case ParentFamilyStatus.OnlyMother: return "Только мать";
                case ParentFamilyStatus.OnlyFather: return "Только отец";
                case ParentFamilyStatus.Relatives: return "Родственники";
                case ParentFamilyStatus.Guardianship: return "Опекунство";
                case ParentFamilyStatus.BoardingSchool: return "Школа-интернат";
                case ParentFamilyStatus.AnOrphan: return "Сирота";
            }

            return string.Empty;
        }

        public static ParentFamilyStatus ToParentFamilyStatusEnum(this string source)
        {
            switch (source)
            {
                case "Полная": return ParentFamilyStatus.Full;
                case "Только мать": return ParentFamilyStatus.OnlyMother;
                case "Только отец": return ParentFamilyStatus.OnlyFather;
                case "Родственники": return ParentFamilyStatus.Relatives;
                case "Опекунство": return ParentFamilyStatus.Guardianship;
                case "Школа-интернат": return ParentFamilyStatus.BoardingSchool;
                case "Сирота": return ParentFamilyStatus.AnOrphan;
            }

            return ParentFamilyStatus.None;
        }
    }
}
