using PupaParserComeback.Presentation.Abstract;
using PupaParserComeback.Presentation.Constants;
using PupaParserComeback.Presentation.Models.Cards;
using System;
using System.Collections.Generic;

namespace PupaParserComeback.Presentation.Models.CardGroups
{
    public class RecruitCardGroup : IValidCardGroup
    {
        public ServiceCard ServiceCard { get; }
        public FirstCardGroup FirstCardGroup { get; }
        public SecondCardGroup SecondCardGroup { get; }
        public ThirdCardGroup ThirdCardGroup { get; }

        public RecruitCardGroup(string personalPhotoDirectoryPath)
        {
            if (string.IsNullOrWhiteSpace(personalPhotoDirectoryPath))
            {
                throw new ArgumentNullException(nameof(personalPhotoDirectoryPath));
            }

            ServiceCard = new ServiceCard();
            FirstCardGroup = new FirstCardGroup(personalPhotoDirectoryPath);
            SecondCardGroup = new SecondCardGroup();
            ThirdCardGroup = new ThirdCardGroup();
        }

        public RecruitCardGroup(ServiceCard serviceCard,
            FirstCardGroup firstCardGroup,
            SecondCardGroup secondCardGroup,
            ThirdCardGroup thirdCardGroup)
        {
            if (serviceCard == null)
            {
                throw new ArgumentNullException(nameof(serviceCard));
            }

            if (firstCardGroup == null)
            {
                throw new ArgumentNullException(nameof(firstCardGroup));
            }

            if (secondCardGroup == null)
            {
                throw new ArgumentNullException(nameof(secondCardGroup));
            }

            if (thirdCardGroup == null)
            {
                throw new ArgumentNullException(nameof(thirdCardGroup));
            }

            ServiceCard = serviceCard;
            FirstCardGroup = firstCardGroup;
            SecondCardGroup = secondCardGroup;
            ThirdCardGroup = thirdCardGroup;
        }

        public bool IsValid
        {
            get
            {
                return string.IsNullOrWhiteSpace(ServiceCard.Error) &&
                       FirstCardGroup.IsValid &&
                       SecondCardGroup.IsValid &&
                       ThirdCardGroup.IsValid;
            }
        }

        public string Error
        {
            get
            {
                var errors = new List<string>()
                {
                    ServiceCard.Error,
                    FirstCardGroup.Error,
                    SecondCardGroup.Error,
                    ThirdCardGroup.Error
                };

                errors.RemoveAll(e => string.IsNullOrWhiteSpace(e));

                return string.Join(SeparatorConstants.CommaSeparator, errors);
            }
        }
    }
}
