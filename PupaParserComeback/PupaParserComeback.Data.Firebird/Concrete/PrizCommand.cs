using PupaParserComeback.Data.Firebird.Abstract;
using PupaParserComeback.Data.Firebird.Dto;
using PupaParserComeback.Data.Firebird.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PupaParserComeback.Data.Firebird.Concrete
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

        public void Insert(PRIZ entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            entity.ID = _dbContextRepository.Context.NextId("G_PRIZ");

            _dbContextRepository.Context.Set<PRIZ>().Add(entity);
        }

        public void Insert(IEnumerable<PRIZ> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            foreach (var entity in entities)
            {
                entity.ID = _dbContextRepository.Context.NextId("G_PRIZ");
            }

            _dbContextRepository.Context.Set<PRIZ>().AddRange(entities);
        }

        public void Delete(int id)
        {
            var entity = _dbContextRepository.Context.Set<PRIZ>().Find(id);
            if (entity != null)
            {
                _dbContextRepository.Context.Set<PRIZ>().Remove(entity);
            }
        }
    }
}
