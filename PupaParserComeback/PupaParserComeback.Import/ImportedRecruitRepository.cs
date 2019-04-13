using PupaParserComeback.Presentation.Interfaces;
using PupaParserComeback.Presentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PupaParserComeback.Import
{
    public class ImportedRecruitRepository : IImportedRecruitRepository
    {
        private readonly List<RecruitShortUIModel> _importedRecruits;

        public ImportedRecruitRepository()
        {
            _importedRecruits = new List<RecruitShortUIModel>();
        }

        public IEnumerable<RecruitShortUIModel> Get(string regionalCollecitonPoint, 
            DateTime? conscriptionDate, 
            string surname)
        {
            IEnumerable<RecruitShortUIModel> filtered = _importedRecruits;

            if (!string.IsNullOrWhiteSpace(regionalCollecitonPoint))
            {
                filtered = filtered.Where(r => r.RegionalCollectionPoint.Trim().ToLower()
                    .StartsWith(regionalCollecitonPoint.Trim().ToLower()));
            }

            if (conscriptionDate.HasValue)
            {
                DateTime outConscriptionDate;
                filtered = filtered.Where(r => DateTime.TryParse(r.ConscriptionDate, out outConscriptionDate)
                    ? outConscriptionDate.Date == conscriptionDate.Value.Date
                    : false);
            }

            if (!string.IsNullOrWhiteSpace(surname))
            {
                filtered = filtered.Where(r => r.Surname.Trim().ToLower()
                    .StartsWith(surname.Trim().ToLower()));
            }

            return filtered;
        }

        public void AddRange(IEnumerable<RecruitShortUIModel> recruitShortUIModels)
        {
            if (recruitShortUIModels == null)
            {
                throw new ArgumentNullException(nameof(recruitShortUIModels));
            }

            foreach (var recruit in recruitShortUIModels)
            {
                if (!_importedRecruits.Contains(recruit))
                {
                    _importedRecruits.Add(recruit);
                }
            }
        }

        public void Clear()
        {
            _importedRecruits.Clear();
        }
    }
}
