using PupaParserComeback.Presentation.Abstract;
using PupaParserComeback.Presentation.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PupaParserComeback.Presentation.RecruitCommands.Parameters
{
    public class UpdateRecruitsCommandParameters : BaseRecruitCommandParameter
    {
        public DateTime? ConscriptionDate { get; }
        public string SelectedRegionalCollectionPoint { get; }
        public string Surname { get; }

        public string SelectedStorage { get; }

        public ObservableCollection<RecruitShortUIModel> RecruitShortUIModels { get; }

        public UpdateRecruitsCommandParameters(DateTime? conscriptionDate,
            string selectedRegionalCollectionPoint,
            string surname,
            string selectedStorage,
            ObservableCollection<RecruitShortUIModel> recruitShortUIModels,
            IStateChanged stateChanged) : base(stateChanged)
        {
            if (recruitShortUIModels == null)
            {
                throw new ArgumentNullException(nameof(recruitShortUIModels));
            }

            if (stateChanged == null)
            {
                throw new ArgumentNullException(nameof(stateChanged));
            }

            ConscriptionDate = conscriptionDate;
            SelectedRegionalCollectionPoint = selectedRegionalCollectionPoint;
            Surname = surname;
            SelectedStorage = selectedStorage;
            RecruitShortUIModels = recruitShortUIModels;
        }
    }
}
