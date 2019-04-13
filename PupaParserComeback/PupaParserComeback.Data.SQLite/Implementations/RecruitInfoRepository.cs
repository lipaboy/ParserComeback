using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PupaParserComeback.Domain.DomainModels;
using PupaParserComeback.Data.SQLite.Abstract;
using PupaParserComeback.Data.SQLite.Dto;
using PupaParserComeback.Data.SQLite.Concrete;
using PupaParserComeback.Domain.Interfaces;
using PupaParserComeback.Data.SQLite.ExtensionMethods;

namespace PupaParserComeback.Data.SQLite.Implementations
{
    public class RecruitInfoRepository : IRecruitInfoRepository
    {
        private IPrizQuery _prizQuery;
        private IPrizCommand _prizCommand;

        public RecruitInfoRepository(IPrizQuery prizQuery, IPrizCommand prizCommand)
        {
            if (prizQuery == null)
            {
                throw new ArgumentNullException(nameof(prizQuery));
            }

            if (prizCommand == null)
            {
                throw new ArgumentNullException(nameof(prizCommand));
            }

            _prizQuery = prizQuery;
            _prizCommand = prizCommand;
        }


        #region Sync Operations

        public IEnumerable<RecruitInfo> Get(string regionalCollecitonPoint, 
            DateTime? conscriptionDate,
            string surname)
        {
            IEnumerable<priz> all = _prizQuery.GetAll();
            IEnumerable<priz> filtered = Filter(all, regionalCollecitonPoint, conscriptionDate, surname);

            return filtered
                .ToList() // load from db in memory
                .Select(p => RecruitInfoMapper.Map(p));
        }

        private IEnumerable<priz> Filter(IEnumerable<priz> all,
            string regionalCollecitonPoint,
            DateTime? conscriptionDate,
            string surname)
        {
            if (!string.IsNullOrWhiteSpace(regionalCollecitonPoint))
            {
                all = all.Where(p => p.rvk.Trim().ToLower()
                    .StartsWith(regionalCollecitonPoint.Trim().ToLower()));
            }

            if (conscriptionDate.HasValue)
            {
                all = all.Where(p =>
                    p.d_advent.GetDateTime().HasValue &&
                    p.d_advent.GetDateTime().Value.Date == conscriptionDate.Value.Date);
            }

            if (!string.IsNullOrWhiteSpace(surname))
            {
                all = all.Where(p => p.surname.Trim().ToLower()
                    .StartsWith(surname.Trim().ToLower()));
            }

            return all;
        }

        public IEnumerable<RecruitInfo> Get(IEnumerable<long> ids)
        {
            return _prizQuery.Get(ids)
                .ToList() // load from db in memory
                .Select(p => RecruitInfoMapper.Map(p));
        }

        public RecruitInfo Get(long id)
        {
            var prizById = _prizQuery.Get(id);
            if (prizById == null) return null;

            return RecruitInfoMapper.Map(prizById);
        }

        public void Add(RecruitInfo recruitInfo)
        {
            if (recruitInfo == null)
            {
                throw new ArgumentNullException(nameof(recruitInfo));
            }

            var priz = RecruitInfoMapper.Map(recruitInfo);

            _prizCommand.Insert(priz);
        }

        public void Change(RecruitInfo recruitInfo)
        {
            if (recruitInfo == null)
            {
                throw new ArgumentNullException(nameof(recruitInfo));
            }

            var priz = RecruitInfoMapper.Map(recruitInfo);

            _prizCommand.Update(priz);
        }

        public void Remove(long id)
        {
            _prizCommand.Delete(id);
        }


        #endregion

        #region Async Operations

        public async Task<IEnumerable<RecruitInfo>> GetAsync(string regionalCollecitonPoint, DateTime? conscriptionDate, string surname)
        {
            return await Task.Run(() =>
            {
                return Get(regionalCollecitonPoint, conscriptionDate, surname);
            })
            .ConfigureAwait(false);
        }

        public async Task<IEnumerable<RecruitInfo>> GetAsync(IEnumerable<long> ids)
        {
            return await Task.Run(() =>
            {
                return Get(ids);
            })
            .ConfigureAwait(false);
        }

        public async Task<RecruitInfo> GetAsync(long id)
        {
            return await Task.Run(() =>
            {
                return Get(id);
            })
            .ConfigureAwait(false);
        }

        public async Task AddAsync(RecruitInfo recruitInfo)
        {
            await Task.Run(() =>
            {
                Add(recruitInfo);
            })
            .ConfigureAwait(false);
        }

        public async Task ChangeAsync(RecruitInfo recruitInfo)
        {
            await Task.Run(() =>
            {
                Change(recruitInfo);
            })
            .ConfigureAwait(false);
        }

        public async Task RemoveAsync(long id)
        {
            await Task.Run(() =>
            {
                Remove(id);
            })
            .ConfigureAwait(false);
        }

        #endregion
    }
}
