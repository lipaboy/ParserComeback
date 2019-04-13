using PupaParserComeback.Domain.Enums;

namespace PupaParserComeback.Domain.ExtensionMethods.EnumExtensions
{
    public static class AccessFormStatusExtensions
    {
        public static string ToAccessFormString(this AccessForm source)
        {
            switch (source)
            {
                case AccessForm.First: return "1";
                case AccessForm.Second: return "2";
                case AccessForm.Third: return "3";
            }

            return string.Empty;
        }

        public static AccessForm ToAccessFormEnum(this string source)
        {
            switch (source)
            {
                case "1": return AccessForm.First;
                case "2": return AccessForm.Second;
                case "3": return AccessForm.Third;
            }

            return AccessForm.None;
        }
    }
}
