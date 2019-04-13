using PupaParserComeback.Presentation.Abstract;
using PupaParserComeback.Presentation.Constants;
using PupaParserComeback.Presentation.Models.Cards;
using System;
using System.Collections.Generic;

namespace PupaParserComeback.Presentation.Models.CardGroups
{
    public class ThirdCardGroup : IValidCardGroup
    {
        public MedicineCard MedicineCard { get; }
        public PhysiologicalCharacteristicsCard PhysiologicalCharacteristicsCard { get; }
        public SportCard SportCard { get; }
        public ContactsCard ContactsCard { get; }
        public FamilyCard FamilyCard { get; }

        public ThirdCardGroup()
        {
            MedicineCard = new MedicineCard();
            PhysiologicalCharacteristicsCard = new PhysiologicalCharacteristicsCard();
            SportCard = new SportCard();
            ContactsCard = new ContactsCard();
            FamilyCard = new FamilyCard();
        }

        public ThirdCardGroup(MedicineCard medicineCard,
            PhysiologicalCharacteristicsCard physiologicalCharacteristicsCard,
            SportCard sportCard,
            ContactsCard contactsCard,
            FamilyCard familyCard)
        {
            if (medicineCard == null)
            {
                throw new ArgumentNullException(nameof(medicineCard));
            }

            if (physiologicalCharacteristicsCard == null)
            {
                throw new ArgumentNullException(nameof(physiologicalCharacteristicsCard));
            }

            if (sportCard == null)
            {
                throw new ArgumentNullException(nameof(sportCard));
            }

            if (contactsCard == null)
            {
                throw new ArgumentNullException(nameof(contactsCard));
            }

            if (familyCard == null)
            {
                throw new ArgumentNullException(nameof(familyCard));
            }

            MedicineCard = medicineCard;
            PhysiologicalCharacteristicsCard = physiologicalCharacteristicsCard;
            SportCard = sportCard;
            ContactsCard = contactsCard;
            FamilyCard = familyCard;
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
                    MedicineCard.Error,
                    PhysiologicalCharacteristicsCard.Error,
                    SportCard.Error,
                    ContactsCard.Error,
                    FamilyCard.Error
                };

                errors.RemoveAll(e => string.IsNullOrWhiteSpace(e));

                return string.Join(SeparatorConstants.CommaSeparator, errors);
            }
        }
    }
}
