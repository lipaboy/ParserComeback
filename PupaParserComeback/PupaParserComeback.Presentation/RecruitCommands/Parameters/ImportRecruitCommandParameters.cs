using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PupaParserComeback.Presentation.Abstract;

namespace PupaParserComeback.Presentation.RecruitCommands.Parameters
{
    public class ImportRecruitCommandParameters : BaseRecruitCommandParameter
    {
        public DateTime? ConscriptionDate { get; }
        public string SelectedRegionalCollectionPoint { get; }

        public ImportRecruitCommandParameters(DateTime? conscriptionDate,
            string selectedRegionalCollectionPoint,
            IStateChanged stateChanged) : base(stateChanged)
        {
            if (stateChanged == null)
            {
                throw new ArgumentNullException(nameof(stateChanged));
            }

            ConscriptionDate = conscriptionDate;
            SelectedRegionalCollectionPoint = selectedRegionalCollectionPoint;
        }
    }
}
