using PupaParserComeback.Presentation.Enums;

namespace PupaParserComeback.Presentation.ExtensionMethods
{
    public static class EnumExtension
    {
        public static string StateResultToString(this StateResult source)
        {
            switch (source)
            {
                case StateResult.Success:
                    return "Успех: ";
                case StateResult.Notify:
                    return "Уведомление: ";
                case StateResult.Cancel:
                    return "Отмена: ";
                case StateResult.Error:
                    return "Ошибка: ";
                default:
                    return string.Empty;
            }
        }

        public static StateResult StateResultToEnum(this string source)
        {
            switch (source)
            {
                case "Успех: ":
                    return StateResult.Success;
                case "Уведомление: ":
                    return StateResult.Notify;
                case "Отмена: ":
                    return StateResult.Cancel;
                case "Ошибка: ":
                    return StateResult.Error;
                default:
                    return StateResult.Empty;
            }
        }
    }
}
