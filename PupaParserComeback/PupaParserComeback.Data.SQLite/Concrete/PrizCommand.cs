using PupaParserComeback.Data.SQLite.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PupaParserComeback.Data.SQLite.Dto;
using System.Data.Entity;

namespace PupaParserComeback.Data.SQLite.Concrete
{
    public class PrizCommand : IPrizCommand
    {
        private readonly IDbContextRepository _dbContextRepository;

        public PrizCommand(IDbContextRepository dbContextRepository)
        {
            if (dbContextRepository == null)
            {
                throw new ArgumentNullException(nameof(dbContextRepository));
            }

            _dbContextRepository = dbContextRepository;
        }

        public void Insert(priz entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _dbContextRepository.Context.Set<priz>().Add(entity);
        }
        
        public void Update(priz entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _dbContextRepository.Context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(long id)
        {
            var entity = _dbContextRepository.Context.Set<priz>().Find(id);
            if (entity != null)
            {
                _dbContextRepository.Context.Set<priz>().Remove(entity);
            }
        }
    }
}
