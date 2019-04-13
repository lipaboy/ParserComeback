using System;

namespace PupaParserComeback.UI.Exceptions
{
    public class ExportException : Exception
    {
        private const string _message = "Произошла ошибка при экспорте.";

        public ExportException(Exception ex) : base(_message, ex) { }
    }
}
