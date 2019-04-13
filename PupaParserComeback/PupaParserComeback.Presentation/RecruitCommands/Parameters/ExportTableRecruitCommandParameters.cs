using PupaParserComeback.Presentation.Abstract;
using PupaParserComeback.Presentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PupaParserComeback.Presentation.RecruitCommands.Parameters
{
    public class ExportTableRecruitCommandParameters : BaseRecruitCommandParameter
    {
        public IEnumerable<RecruitShortUIModel> RecruitShortUIModels { get; }
        public string ConscriptionDate { get; }
        public string RegionalCollectionPoint { get; }

        public ExportTableRecruitCommandParameters(IEnumerable<RecruitShortUIModel> recruitShortUIModels, 
            string conscriptionDate, 
            string regionalCollectionPoint,
            IStateChanged stateChanged) : base(stateChanged)
        {
            if (recruitShortUIModels == null)
            {
                throw new ArgumentNullException(nameof(recruitShortUIModels));
            }

            RecruitShortUIModels = recruitShortUIModels;
            ConscriptionDate = conscriptionDate;
            RegionalCollectionPoint = regionalCollectionPoint;
        }
    }
}
