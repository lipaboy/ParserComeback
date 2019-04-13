using PupaParserComeback.Domain.Enums;

namespace PupaParserComeback.Domain.ExtensionMethods.EnumExtensions
{
    public static class OfficialStatusExtensions
    {
        public static string ToOfficialStatusString(this OfficialStatus source)
        {
            switch (source)
            {
                case OfficialStatus.Team: return "Командные";
                case OfficialStatus.Operator: return "Операторские";
                case OfficialStatus.CommunicationAndObservation: return "Связи и наблюдения";
                case OfficialStatus.Driver: return "Водительские";
                case OfficialStatus.Technological: return "Технологические";
                case OfficialStatus.Special: return "Спецназначения";
                case OfficialStatus.ToStudyAtUniversity: return "К обучению в ВУЗ";
            }

            return string.Empty;
        }

        public static OfficialStatus ToOfficialStatusEnum(this string source)
        {
            switch (source)
            {
                case "Командные": return OfficialStatus.Team;
                case "Операторские": return OfficialStatus.Operator;
                case "Связи и наблюдения": return OfficialStatus.CommunicationAndObservation;
                case "Водительские": return OfficialStatus.Driver;
                case "Технологические": return OfficialStatus.Technological;
                case "Спецназначения": return OfficialStatus.Special;
                case "К обучению в ВУЗ": return OfficialStatus.ToStudyAtUniversity;
            }

            return OfficialStatus.None;
        }
    }
}
