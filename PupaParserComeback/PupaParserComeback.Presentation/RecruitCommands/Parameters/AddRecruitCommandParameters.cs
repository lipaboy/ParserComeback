using PupaParserComeback.Domain.DomainModels;
using PupaParserComeback.Presentation.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PupaParserComeback.Presentation.RecruitCommands.Parameters
{
    public class AddRecruitCommandParameters : BaseRecruitCommandParameter
    {
        public RecruitInfo RecruitInfo { get; }

        public AddRecruitCommandParameters(RecruitInfo recruitInfo, 
            IStateChanged stateChanged) : base(stateChanged)
        {
            if (recruitInfo == null)
            {
                throw new ArgumentNullException(nameof(recruitInfo));
            }

            RecruitInfo = recruitInfo;
        }
    }
}
