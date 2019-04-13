using PupaParserComeback.Domain.Enums;

namespace PupaParserComeback.Domain.ExtensionMethods.EnumExtensions
{
    public static class NervouslyPsychologicalStatusExtensions
    {
        public static string ToNervouslyPsychologicalStatusString(this NervouslyPsychologicalStatus source)
        {
            switch (source)
            {
                case NervouslyPsychologicalStatus.High: return "Высокая";
                case NervouslyPsychologicalStatus.Good: return "Хорошая";
                case NervouslyPsychologicalStatus.Satisfactory: return "Удовлетворительная";
                case NervouslyPsychologicalStatus.NoSatisfactory: return "Неудовлетворительная";
            }

            return string.Empty;
        }

        public static NervouslyPsychologicalStatus ToNervouslyPsychologicalStatusEnum(this string source)
        {
            switch (source)
            {
                case "Высокая": return NervouslyPsychologicalStatus.High;
                case "Хорошая": return NervouslyPsychologicalStatus.Good;
                case "Удовлетворительная": return NervouslyPsychologicalStatus.Satisfactory;
                case "Неудовлетворительная": return NervouslyPsychologicalStatus.NoSatisfactory;
            }

            return NervouslyPsychologicalStatus.None;
        }
    }
}
