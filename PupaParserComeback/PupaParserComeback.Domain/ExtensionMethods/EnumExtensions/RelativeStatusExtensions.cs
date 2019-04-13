using PupaParserComeback.Domain.Enums;

namespace PupaParserComeback.Domain.ExtensionMethods.EnumExtensions
{
    public static class RelativeStatusExtensions
    {
        public static string ToRelativeStatusString(this RelativeStatus source)
        {
            switch (source)
            {
                case RelativeStatus.Mother: return "Мать";
                case RelativeStatus.Father: return "Отец";
                case RelativeStatus.Grandmother: return "Бабушка";
                case RelativeStatus.Grandfather: return "Дедушка";
                case RelativeStatus.Brother: return "Брат";
                case RelativeStatus.Sister: return "Сестра";
                case RelativeStatus.Wife: return "Жена";
                case RelativeStatus.Son: return "Сын";
                case RelativeStatus.Daughter: return "Дочь";
                case RelativeStatus.Uncle: return "Дядя";
                case RelativeStatus.Aunt: return "Тетя";
                case RelativeStatus.Guardian: return "Опекун";
                case RelativeStatus.Stepmother: return "Мачеха";
                case RelativeStatus.Stepfather: return "Отчим";
                case RelativeStatus.Stepson: return "Пасынок";
                case RelativeStatus.Stepdaughter: return "Падчерица";
                case RelativeStatus.MotherInLaw: return "Теща";
                case RelativeStatus.FatherInLaw: return "Тесть";
            }

            return string.Empty;
        }

        public static RelativeStatus ToRelativeStatusEnum(this string source)
        {
            switch (source)
            {
                case "Мать": return RelativeStatus.Mother;
                case "Отец": return RelativeStatus.Father;
                case "Бабушка": return RelativeStatus.Grandmother;
                case "Дедушка": return RelativeStatus.Grandfather;
                case "Брат": return RelativeStatus.Brother;
                case "Сестра": return RelativeStatus.Sister;
                case "Жена": return RelativeStatus.Wife;
                case "Сын": return RelativeStatus.Son;
                case "Дочь": return RelativeStatus.Daughter;
                case "Дядя": return RelativeStatus.Uncle;
                case "Тетя": return RelativeStatus.Aunt;
                case "Опекун": return RelativeStatus.Guardian;
                case "Мачеха": return RelativeStatus.Stepmother;
                case "Отчим": return RelativeStatus.Stepfather;
                case "Пасынок": return RelativeStatus.Stepson;
                case "Падчерица": return RelativeStatus.Stepdaughter;
                case "Теща": return RelativeStatus.MotherInLaw;
                case "Тесть": return RelativeStatus.FatherInLaw;
            }

            return RelativeStatus.None;
        }
    }
}
