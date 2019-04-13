using PupaParserComeback.Domain.Enums;

namespace PupaParserComeback.Domain.ExtensionMethods.EnumExtensions
{
    public static class CriminalStatusExtensions
    {
        public static string ToCriminalStatusString(this CriminalStatus source)
        {
            switch (source)
            {
                case CriminalStatus.HaveNot: return "Не имеет";
                case CriminalStatus.Unclear: return "Неснятая";
                case CriminalStatus.InProcess: return "Отбывает наказание";
                case CriminalStatus.Checking: return "Ведется дознание";
                case CriminalStatus.Clear: return "Снятая";
            }

            return string.Empty;
        }

        public static CriminalStatus ToCriminalStatusEnum(this string source)
        {
            switch (source)
            {
                case "Не имеет": return CriminalStatus.HaveNot;
                case "Неснятая": return CriminalStatus.Unclear;
                case "Отбывает наказание": return CriminalStatus.InProcess;
                case "Ведется дознание": return CriminalStatus.Checking;
                case "Снятая": return CriminalStatus.Clear;
            }

            return CriminalStatus.None;
        }
    }
}
