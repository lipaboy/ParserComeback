using PupaParserComeback.Presentation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PupaParserComeback.Presentation.Enums;
using PupaParserComeback.Presentation.Models.CardGroups;
using PupaParserComeback.Presentation.Constants;
using PupaParserComeback.Domain.DomainModels;
using PupaParserComeback.Presentation.Mappers;
using PupaParserComeback.Presentation.Models;
using PupaParserComeback.Domain.Interfaces;
using PupaParserComeback.Presentation.EventArguments;

namespace PupaParserComeback.Presentation.RecruitFactories
{
    public class RecruitCardGroupFactory : IRecruitCardGroupFactory
    {
        private readonly string _personalPhotoDirectoryPath;
        private readonly RecruitCardGroup _recruitCardGroupByAdd;
        private readonly IRecruitImporter _recruitImporter;
        private readonly IRecruitInfoRepository _recruitInfoRepository;

        public RecruitCardGroupFactory(string personalPhotoDirectoryPath,
            RecruitCardGroup recruitCardGroupByAdd,
            IRecruitImporter recruitImporter,
            IRecruitInfoRepository recruitInfoRepository)
        {
            if (string.IsNullOrWhiteSpace(personalPhotoDirectoryPath))
            {
                throw new ArgumentNullException(nameof(personalPhotoDirectoryPath));
            }

            if (recruitCardGroupByAdd == null)
            {
                throw new ArgumentNullException(nameof(recruitCardGroupByAdd));
            }

            if (recruitImporter == null)
            {
                throw new ArgumentNullException(nameof(recruitImporter));
            }

            if (recruitInfoRepository == null)
            {
                throw new ArgumentNullException(nameof(recruitInfoRepository));
            }

            _personalPhotoDirectoryPath = personalPhotoDirectoryPath;
            _recruitCardGroupByAdd = recruitCardGroupByAdd;
            _recruitImporter = recruitImporter;
            _recruitInfoRepository = recruitInfoRepository;
        }

        public RecruitCardGroup Create(RecruitOperationEventArgs recruitOperationEventArgs)
        {
            switch (recruitOperationEventArgs.RecruitOperation)
            {
                case RecruitOperation.Import:
                    {
                        return _recruitImporter.ImportRecruitCardGroup(recruitOperationEventArgs.RecruitShortUIModel);
                    }
                case RecruitOperation.Add:
                    {
                        return _recruitCardGroupByAdd;
                    }
                case RecruitOperation.Edit:
                    {
                        if (recruitOperationEventArgs.RecruitShortUIModel != null && 
                            recruitOperationEventArgs.RecruitShortUIModel.SqliteId.HasValue)
                        {
                            var id = recruitOperationEventArgs.RecruitShortUIModel.SqliteId.Value;
                            var recruit = _recruitInfoRepository.Get(id);

                            if (recruit == null)
                            {
                                return EmptyCard;
                            }

                            var cardGroupMapper = new CardGroupMapper(_personalPhotoDirectoryPath, recruit);
                            return cardGroupMapper.Map();
                        }
                        else
                        {
                            return EmptyCard;
                        }
                    }
            }

            return EmptyCard;
        }

        private RecruitCardGroup EmptyCard
        {
            get { return _recruitCardGroupByAdd; }
        }
    }
}
