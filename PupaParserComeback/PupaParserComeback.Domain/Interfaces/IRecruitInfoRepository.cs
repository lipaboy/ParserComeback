using PupaParserComeback.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PupaParserComeback.Domain.Interfaces
{
    public interface IRecruitInfoRepository
    { 
        #region Sync Operations

        IEnumerable<RecruitInfo> Get(string regionalCollecitonPoint, DateTime? conscriptionDate, string surname);
        IEnumerable<RecruitInfo> Get(IEnumerable<long> ids);
        RecruitInfo Get(long id);

        void Add(RecruitInfo recruitInfo);
        void Change(RecruitInfo recruitInfo);
        void Remove(long id);

        #endregion

        #region Async Operations

        Task<IEnumerable<RecruitInfo>> GetAsync(string regionalCollecitonPoint, DateTime? conscriptionDate, string surname);
        Task<IEnumerable<RecruitInfo>> GetAsync(IEnumerable<long> ids);
        Task<RecruitInfo> GetAsync(long id);

        Task AddAsync(RecruitInfo recruitInfo);
        Task ChangeAsync(RecruitInfo recruitInfo);
        Task RemoveAsync(long id);

        #endregion
    }
}
