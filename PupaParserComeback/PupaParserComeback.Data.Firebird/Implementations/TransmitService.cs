using PupaParserComeback.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PupaParserComeback.Domain.DomainModels;
using PupaParserComeback.Data.Firebird.Abstract;
using PupaParserComeback.Data.Firebird.Dto;
using PupaParserComeback.Data.Firebird.Concrete;

namespace PupaParserComeback.Data.Firebird.Implementations
{
    public class TransmitService : ITransmitService
    {
        private IPrizCommand _prizCommand;

        public TransmitService(IPrizCommand prizCommand)
        {
            if (prizCommand == null)
            {
                throw new ArgumentNullException(nameof(prizCommand));
            }

            _prizCommand = prizCommand;
        }

        #region Sync Operations

        public void Move(IEnumerable<RecruitInfo> recruitInfoes)
        {
            if (recruitInfoes == null)
            {
                throw new ArgumentNullException(nameof(recruitInfoes));
            }

            var prizRange = recruitInfoes.Select(r => RecruitInfoMapper.Map(r)).ToList();

            _prizCommand.Insert(prizRange);

            UpdateIds(recruitInfoes, prizRange);
        }

        private void UpdateIds(IEnumerable<RecruitInfo> recruitInfoes, List<PRIZ> prizRange)
        {
            int idx = 0;
            foreach (var recruitInfo in recruitInfoes)
            {
                recruitInfo.ServiceInfo.ChangeFirebirdId(prizRange[idx++].ID);
            }
        }

        #endregion

        #region Async Operations

        public async Task MoveAsync(IEnumerable<RecruitInfo> recruitInfoes)
        {
            await Task.Run(() =>
            {
                Move(recruitInfoes);
            })
            .ConfigureAwait(false);
        }

        #endregion
    }
}
