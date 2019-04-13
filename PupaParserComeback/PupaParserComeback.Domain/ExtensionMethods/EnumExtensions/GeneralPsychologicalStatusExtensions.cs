using PupaParserComeback.Domain.Enums;

namespace PupaParserComeback.Domain.ExtensionMethods.EnumExtensions
{
    public static class GeneralPsychologicalStatusExtensions
    {
        public static string ToGeneralPsychologicalStatusString(this GeneralPsychologicalStatus source)
        {
            switch (source)
            {
                case GeneralPsychologicalStatus.High: return "Высокий";
                case GeneralPsychologicalStatus.Good: return "Хороший";
                case GeneralPsychologicalStatus.Satisfactory: return "Удовлетворительный";
                case GeneralPsychologicalStatus.NoSatisfactory: return "Неудовлетворительный";
            }

            return string.Empty;
        }

        public static GeneralPsychologicalStatus ToGeneralPsychologicalStatusEnum(this string source)
        {
            switch (source)
            {
                case "Высокий": return GeneralPsychologicalStatus.High;
                case "Хороший": return GeneralPsychologicalStatus.Good;
                case "Удовлетворительный": return GeneralPsychologicalStatus.Satisfactory;
                case "Неудовлетворительный": return GeneralPsychologicalStatus.NoSatisfactory;
            }

            return GeneralPsychologicalStatus.None;
        }
    }
}
