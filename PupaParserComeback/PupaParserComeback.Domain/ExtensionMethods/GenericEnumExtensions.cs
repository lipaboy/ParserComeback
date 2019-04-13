using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PupaParserComeback.Domain.ExtensionMethods
{
    public static class GenericEnumExtensions
    {
        public static T ToEnum<T>(this string source)
        {
            return (T)Enum.Parse(typeof(T), source);
        }

        public static IEnumerable<string> GetStringValues<T>(bool withEmptyValue = false)
        {
            var enumValues = Enum.GetValues(typeof(T)).Cast<T>()
                .Select(s => s.ToString())
                .ToList();

            var stringValues = new List<string>();

            if (withEmptyValue)
            {
                stringValues.Add(string.Empty);
            }

            stringValues.AddRange(enumValues);

            return stringValues;
        }
    }
}
