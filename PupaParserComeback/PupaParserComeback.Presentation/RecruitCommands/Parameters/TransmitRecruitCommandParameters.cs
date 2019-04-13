using PupaParserComeback.Domain.DomainModels;
using PupaParserComeback.Presentation.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PupaParserComeback.Presentation.RecruitCommands.Parameters
{
    public class TransmitRecruitCommandParameters : BaseRecruitCommandParameter
    {
        public IEnumerable<long> RecruitIds { get; }

        public TransmitRecruitCommandParameters(IEnumerable<long> recruitIds,
            IStateChanged stateChanged) : base(stateChanged)
        {
            if (recruitIds == null)
            {
                throw new ArgumentNullException(nameof(recruitIds));
            }

            RecruitIds = recruitIds;
        }
    }
}
