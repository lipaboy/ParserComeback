using PupaParserComeback.Presentation.Models;
using System;
using System.Collections.Generic;

namespace PupaParserComeback.Presentation.Interfaces
{
    public interface IImportedRecruitRepository
    {
        IEnumerable<RecruitShortUIModel> Get(string regionalCollecitonPoint, DateTime? conscriptionDate, string surname);
        void AddRange(IEnumerable<RecruitShortUIModel> recruitShortUIModels);
        void Clear();
    }
}
