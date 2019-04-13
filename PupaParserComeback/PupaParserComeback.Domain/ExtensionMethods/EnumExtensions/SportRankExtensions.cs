using PupaParserComeback.Domain.Enums;

namespace PupaParserComeback.Domain.ExtensionMethods.EnumExtensions
{
    public static class SportRankExtensions
    {
        public static string ToSportRankString(this SportRank source)
        {
            switch (source)
            {
                case SportRank.HaveNot: return "Не имеет";
                case SportRank.First: return "1-й";
                case SportRank.Second: return "2-й";
                case SportRank.Third: return "3-й";
                case SportRank.CMS: return "КМС";
                case SportRank.MS: return "МС";
                case SportRank.MSIC: return "МСМК";
            }

            return string.Empty;
        }

        public static SportRank ToSportRankEnum(this string source)
        {
            switch (source)
            {
                case "Не имеет": return SportRank.HaveNot;
                case "1-й": return SportRank.First;
                case "2-й": return SportRank.Second;
                case "3-й": return SportRank.Third;
                case "КМС": return SportRank.CMS;
                case "МС": return SportRank.MS;
                case "МСМК": return SportRank.MSIC;
            }

            return SportRank.None;
        }
    }
}
