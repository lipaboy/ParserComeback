using PupaParserComeback.Presentation.Enums;
using PupaParserComeback.Presentation.Models;
using System;

namespace PupaParserComeback.Presentation.EventArguments
{
    public class RecruitOperationEventArgs : EventArgs
    {
        public RecruitOperation RecruitOperation { get; }
        public RecruitShortUIModel RecruitShortUIModel { get; }

        public RecruitOperationEventArgs(RecruitOperation recruitOperation, RecruitShortUIModel recruitShortUIModel = null)
        {
            RecruitOperation = recruitOperation;
            RecruitShortUIModel = recruitShortUIModel;
        }
    }
}
