using PupaParserComeback.Data.SQLite.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PupaParserComeback.Data.SQLite.Abstract
{
    public interface IPrizCommand
    {
        void Insert(priz entity);
        void Update(priz entity);
        void Delete(long id);
    }
}
