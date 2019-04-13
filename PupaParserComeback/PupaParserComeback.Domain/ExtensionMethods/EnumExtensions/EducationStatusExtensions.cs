using PupaParserComeback.Domain.Enums;

namespace PupaParserComeback.Domain.ExtensionMethods.EnumExtensions
{
    public static class EducationStatusExtensions
    {
        public static string ToEducationStatusString(this EducationStatus source)
        {
            switch (source)
            {
                case EducationStatus.Illiterate: return "Неграмотный";
                case EducationStatus.Elementary: return "Начальное";
                case EducationStatus.Basic: return "Основное";
                case EducationStatus.Secondary: return "Среднее (полное)";
                case EducationStatus.ElementaryVocational: return "Начальное профессиональное";
                case EducationStatus.SecondaryVocational: return "Среднее профессиональное";
                case EducationStatus.HigherVocational: return "Высшее профессиональное";
            }

            return string.Empty;
        }

        public static EducationStatus ToEducationStatusEnum(this string source)
        {
            switch (source)
            {
                case "Неграмотный": return EducationStatus.Illiterate;
                case "Начальное": return EducationStatus.Elementary;
                case "Основное": return EducationStatus.Basic;
                case "Среднее (полное)": return EducationStatus.Secondary;
                case "Начальное профессиональное": return EducationStatus.ElementaryVocational;
                case "Среднее профессиональное": return EducationStatus.SecondaryVocational;
                case "Высшее профессиональное": return EducationStatus.HigherVocational;
            }

            return EducationStatus.None;
        }
    }
}
