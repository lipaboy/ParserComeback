using PupaParserComeback.Domain.Enums;

namespace PupaParserComeback.Domain.ExtensionMethods.EnumExtensions
{
    public static class RegisterStatusExtensions
    {
        public static string ToRegisterStatusString(this RegisterStatus source)
        {
            switch (source)
            {
                case RegisterStatus.WasNot: return "Не состоял";
                case RegisterStatus.Drug: return "В наркодиспансере";
                case RegisterStatus.Psych: return "В психдиспансере";
                case RegisterStatus.Police: return "В полиции";
            }

            return string.Empty;
        }

        public static RegisterStatus ToRegisterStatusEnum(this string source)
        {
            switch (source)
            {
                case "Не состоял": return RegisterStatus.WasNot;
                case "В наркодиспансере": return RegisterStatus.Drug;
                case "В психдиспансере": return RegisterStatus.Psych;
                case "В полиции": return RegisterStatus.Police;
            }

            return RegisterStatus.None;
        }
    }
}
