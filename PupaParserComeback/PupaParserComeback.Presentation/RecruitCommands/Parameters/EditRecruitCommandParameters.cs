using PupaParserComeback.Domain.DomainModels;
using PupaParserComeback.Presentation.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PupaParserComeback.Presentation.RecruitCommands.Parameters
{
    public class EditRecruitCommandParameters : BaseRecruitCommandParameter
    {
        public RecruitInfo RecruitInfo { get; }

        public EditRecruitCommandParameters(RecruitInfo recruitInfo,
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
