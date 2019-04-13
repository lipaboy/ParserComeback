using System;

namespace PupaParserComeback.UI.Exceptions
{
    public class DomainException : Exception
    {
        private const string _message = "Произошла ошибка при обработке данных.";

        public DomainException(Exception ex) : base(_message, ex) { }
    }
}
