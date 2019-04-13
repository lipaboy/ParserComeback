using PupaParserComeback.Presentation.Abstract;
using PupaParserComeback.Presentation.Constants;
using PupaParserComeback.Presentation.Models.Cards;
using System;
using System.Collections.Generic;

namespace PupaParserComeback.Presentation.Models.CardGroups
{
    public class FirstCardGroup : IValidCardGroup
    {
        public PassportInfoCard PassportInfoCard { get; }
        public PassportPersonInfoCard PassportPersonInfoCard { get; }
        public PassportAccommodationCard PassportAccommodationCard { get; }
        public PassportFamilyInfoCard PassportFamilyInfoCard { get; }
        public CriminalCard CriminalCard { get; }

        public FirstCardGroup(string personalPhotoDirectoryPath)
        {
            if (string.IsNullOrWhiteSpace(personalPhotoDirectoryPath))
            {
                throw new ArgumentNullException(nameof(personalPhotoDirectoryPath));
            }

            PassportInfoCard = new PassportInfoCard();
            PassportPersonInfoCard = new PassportPersonInfoCard(personalPhotoDirectoryPath);
            PassportAccommodationCard = new PassportAccommodationCard();
            PassportFamilyInfoCard = new PassportFamilyInfoCard();
            CriminalCard = new CriminalCard();
        }

        public FirstCardGroup(PassportInfoCard passportInfoCard,
            PassportPersonInfoCard passportPersonInfoCard,
            PassportAccommodationCard passportAccommodationCard,
            PassportFamilyInfoCard passportFamilyInfoCard,
            CriminalCard criminalCard)
        {
            if (passportInfoCard == null)
            {
                throw new ArgumentNullException(nameof(passportInfoCard));
            }

            if (passportPersonInfoCard == null)
            {
                throw new ArgumentNullException(nameof(passportPersonInfoCard));
            }

            if (passportAccommodationCard == null)
            {
                throw new ArgumentNullException(nameof(passportPersonInfoCard));
            }

            if (passportFamilyInfoCard == null)
            {
                throw new ArgumentNullException(nameof(passportPersonInfoCard));
            }

            if (criminalCard == null)
            {
                throw new ArgumentNullException(nameof(passportPersonInfoCard));
            }

            PassportInfoCard = passportInfoCard;
            PassportPersonInfoCard = passportPersonInfoCard;
            PassportAccommodationCard = passportAccommodationCard;
            PassportFamilyInfoCard = passportFamilyInfoCard;
            CriminalCard = criminalCard;
        }

        public bool IsValid
        {
            get
            {
                return string.IsNullOrWhiteSpace(Error);
            }
        }

        public string Error
        {
            get
            {
                var errors = new List<string>()
                {
                    PassportInfoCard.Error,
                    PassportPersonInfoCard.Error,
                    PassportAccommodationCard.Error,
                    PassportFamilyInfoCard.Error,
                    CriminalCard.Error
                };

                errors.RemoveAll(e => string.IsNullOrWhiteSpace(e));

                return string.Join(SeparatorConstants.CommaSeparator, errors);
            }
        }
    }
}
