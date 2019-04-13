using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PupaParserComeback.Data.SQLite.Exceptions
{
    public class AlreadyExistsException : Exception
    {
        private const string _message = "Уже существует в базе данных.";

        public AlreadyExistsException(Exception ex) : base(_message, ex) { }
    }
}
