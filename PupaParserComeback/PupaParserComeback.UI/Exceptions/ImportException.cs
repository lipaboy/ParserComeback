using System;

namespace PupaParserComeback.UI.Exceptions
{
    public class ImportException : Exception
    {
        private const string _message = "Произошла ошибка при импорте.";

        public ImportException(Exception ex) : base(_message, ex) { }
    }
}
