using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PupaParserComeback.Data.SQLite.Abstract
{
    public interface IDbContextFactory
    {
        DbContext Create();
    }
}
