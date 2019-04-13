using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PupaParserComeback.Domain.DomainModels.Military;
using PupaParserComeback.Domain.Enums;
using PupaParserComeback.Domain.DomainModels.Common;

namespace PupaParserComeback.Domain.Test
{
    [TestClass]
    public class MilitaryTest
    {
        [TestMethod]
        public void SecretAccessTest()
        {
            var accessForm = AccessForm.First;
            var number = "1488";
            var issueDate = new DateTime(2017, 1, 1);

            var secretAccess = new SecretAccess(accessForm, number, issueDate);

            Assert.AreEqual(accessForm, secretAccess.AccessForm);
            Assert.AreEqual(number, secretAccess.SecretAccessNumber);
            Assert.AreEqual(issueDate, secretAccess.IssueDate);
        }

        [TestMethod]
        public void MilitaryBilletNotHaveAccessTest()
        {
            var billetNumber = new Code("АН 2281488");
            var militaryBillet = new MilitaryBillet(billetNumber);

            Assert.AreEqual(billetNumber, militaryBillet.BilletNumber);
            Assert.IsNull(militaryBillet.SecretAccess);
            Assert.IsFalse(militaryBillet.IsHaveSecretAccess);
        }

        [TestMethod]
        public void MilitaryBilletHaveAccessTest()
        {
            var billetNumber = new Code("АН 2281488");

            var accessForm = AccessForm.First;
            var number = "1488";
            var issueDate = new DateTime(2017, 1, 1);
            var secretAccess = new SecretAccess(accessForm, number, issueDate);
            
            var militaryBillet = new MilitaryBillet(billetNumber, secretAccess);

            Assert.AreEqual(billetNumber, militaryBillet.BilletNumber);

            Assert.IsTrue(militaryBillet.IsHaveSecretAccess);
            Assert.AreEqual(accessForm, militaryBillet.SecretAccess.AccessForm);
            Assert.AreEqual(number, militaryBillet.SecretAccess.SecretAccessNumber);
            Assert.AreEqual(issueDate, militaryBillet.SecretAccess.IssueDate);
        }

        [TestMethod]
        public void MilitaryProficiencyCardTest()
        {
            var proficiencyCategory = ProficiencyCategory.Second;
            var officialStatus = OfficialStatus.Driver;
            var nervously = NervouslyPsychologicalStatus.Satisfactory;
            var general = GeneralPsychologicalStatus.High;

            var proficiencyCard = new ProficiencyCardInfo(proficiencyCategory, officialStatus, nervously, general);

            Assert.AreEqual(proficiencyCategory, proficiencyCard.ProficiencyCategory);
            Assert.AreEqual(officialStatus, proficiencyCard.OfficialStatus);
            Assert.AreEqual(general, proficiencyCard.GeneralPsychologicalStability);
            Assert.AreEqual(nervously, proficiencyCard.NervouslyPsychologicalStability);
        }

        [TestMethod]
        public void MilitaryInfoTest()
        {
            var personalNumber = new Code("ВС 052572");
            var billetNumber = new Code("АН 2281488");
            var accessForm = AccessForm.First;
            var number = "1488";
            var issueDate = new DateTime(2017, 1, 1);
            var secretAccess = new SecretAccess(accessForm, number, issueDate);
            var militaryBillet = new MilitaryBillet(billetNumber, secretAccess);
            
            var proficiencyCategory = ProficiencyCategory.Second;
            var officialStatus = OfficialStatus.Driver;
            var nervously = NervouslyPsychologicalStatus.Satisfactory;
            var general = GeneralPsychologicalStatus.High;
            var proficiencyCard = new ProficiencyCardInfo(proficiencyCategory, officialStatus, nervously, general);

            var speciality = MilitaryInfo.NoSpeciality;
            var teamMode = "К-220А";

            var militaryInfo = new MilitaryInfo(personalNumber, militaryBillet, proficiencyCard, speciality, teamMode);

            Assert.AreEqual(personalNumber, militaryInfo.PersonalNumber);

            Assert.AreEqual(billetNumber, militaryInfo.Billet.BilletNumber);
            Assert.IsTrue(militaryInfo.Billet.IsHaveSecretAccess);
            Assert.AreEqual(accessForm, militaryInfo.Billet.SecretAccess.AccessForm);
            Assert.AreEqual(number, militaryInfo.Billet.SecretAccess.SecretAccessNumber);
            Assert.AreEqual(issueDate, militaryInfo.Billet.SecretAccess.IssueDate);

            Assert.AreEqual(proficiencyCategory, militaryInfo.ProficiencyCard.ProficiencyCategory);
            Assert.AreEqual(officialStatus, militaryInfo.ProficiencyCard.OfficialStatus);
            Assert.AreEqual(general, militaryInfo.ProficiencyCard.GeneralPsychologicalStability);
            Assert.AreEqual(nervously, militaryInfo.ProficiencyCard.NervouslyPsychologicalStability);

            Assert.AreEqual(speciality, militaryInfo.Speciality);
            Assert.AreEqual(teamMode, militaryInfo.TeamMode);
        }
    }
}
