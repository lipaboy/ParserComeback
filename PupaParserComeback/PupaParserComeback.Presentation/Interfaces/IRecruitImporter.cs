using PupaParserComeback.Presentation.Models;
using PupaParserComeback.Presentation.Models.CardGroups;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PupaParserComeback.Presentation.Interfaces
{
    public interface IRecruitImporter
    {
        string ImportDirectoryPath { get; }
        string PersonalPhotoDirectoryPath { get; }

        void ChangeImportDirectoryPath(string importDirectoryPath);
        void ChangePersonalPhotoDirectoryPath(string personalPhotoDirectoryPath);

        void CopyPersonalPhotos(DateTime? conscriptionDate = null,
            string selectedRegionalCollectionPoint = null);
        IEnumerable<RecruitShortUIModel> ImportRecruitShortUIModels(DateTime? conscriptionDate = null, 
            string selectedRegionalCollectionPoint = null);

        RecruitCardGroup ImportRecruitCardGroup(RecruitShortUIModel recruitShortUIModel);

        Task CopyPersonalPhotosAsync(DateTime? conscriptionDate = null,
            string selectedRegionalCollectionPoint = null);
        Task<IEnumerable<RecruitShortUIModel>> ImportRecruitShortUIModelsAsync(DateTime? conscriptionDate = null,
            string selectedRegionalCollectionPoint = null);
    }
}
