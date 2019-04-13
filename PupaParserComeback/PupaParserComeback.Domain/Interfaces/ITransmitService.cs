using PupaParserComeback.Domain.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PupaParserComeback.Domain.Interfaces
{
    public interface ITransmitService
    {
        #region Sync Operations

        void Move(IEnumerable<RecruitInfo> recruitInfoes);

        #endregion

        #region Async Operations

        Task MoveAsync(IEnumerable<RecruitInfo> recruitInfoes);

        #endregion
    }
}
