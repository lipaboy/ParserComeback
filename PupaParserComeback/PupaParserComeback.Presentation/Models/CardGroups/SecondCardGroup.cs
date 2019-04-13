using PupaParserComeback.Presentation.Abstract;
using PupaParserComeback.Presentation.Constants;
using PupaParserComeback.Presentation.Models.Cards;
using System;
using System.Collections.Generic;

namespace PupaParserComeback.Presentation.Models.CardGroups
{
    public class SecondCardGroup : IValidCardGroup
    {
        public MilitaryDocumentCard MilitaryDocumentCard { get; }
        public ProficiencyCard ProficiencyCard { get; }
        public DriverCard DriverCard { get; }
        public DistributionCard DistributionCard { get; }
        public CivilCard CivilCard { get; }

        public SecondCardGroup()
        {
            MilitaryDocumentCard = new MilitaryDocumentCard();
            ProficiencyCard = new ProficiencyCard();
            DriverCard = new DriverCard();
            DistributionCard = new DistributionCard();
            CivilCard = new CivilCard();
        }

        public SecondCardGroup(MilitaryDocumentCard militaryDocumentCard,
            ProficiencyCard proficiencyCard,
            DriverCard driverCard,
            DistributionCard distributionCard,
            CivilCard civilCard)
        {
            if (militaryDocumentCard == null)
            {
                throw new ArgumentNullException(nameof(militaryDocumentCard));
            }

            if (proficiencyCard == null)
            {
                throw new ArgumentNullException(nameof(proficiencyCard));
            }

            if (driverCard == null)
            {
                throw new ArgumentNullException(nameof(driverCard));
            }

            if (distributionCard == null)
            {
                throw new ArgumentNullException(nameof(distributionCard));
            }

            if (civilCard == null)
            {
                throw new ArgumentNullException(nameof(civilCard));
            }

            MilitaryDocumentCard = militaryDocumentCard;
            ProficiencyCard = proficiencyCard;
            DriverCard = driverCard;
            DistributionCard = distributionCard;
            CivilCard = civilCard;
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
                    MilitaryDocumentCard.Error,
                    ProficiencyCard.Error,
                    DriverCard.Error,
                    DistributionCard.Error,
                    CivilCard.Error
                };

                errors.RemoveAll(e => string.IsNullOrWhiteSpace(e));

                return string.Join(SeparatorConstants.CommaSeparator, errors);
            }
        }
    }
}
