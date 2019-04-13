using PupaParserComeback.Data.SQLite.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PupaParserComeback.Data.SQLite.Abstract
{
    public interface ILogCommand
    {
        void Insert(log message);
    }
}
