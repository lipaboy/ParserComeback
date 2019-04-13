using log4net.Layout.Pattern;
using log4net.Core;
using System.IO;

namespace PupaParserComeback.UI
{
    public class ExceptionPatternLayoutConverter : PatternLayoutConverter
    {
        protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            var exData = loggingEvent.ExceptionObject.Data;

            if (exData != null)
            {
                foreach (var exKey in exData.Keys)
                {
                    var exValue = exData[exKey];

                    writer.Write($"{exKey}: {exValue}");
                }
            }
        }
    }
}
