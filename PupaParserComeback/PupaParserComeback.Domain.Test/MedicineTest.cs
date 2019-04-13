using Microsoft.VisualStudio.TestTools.UnitTesting;
using PupaParserComeback.Domain.DomainModels.Medicine;
using PupaParserComeback.Domain.Enums;

namespace PupaParserComeback.Domain.Test
{
    [TestClass]
    public class MedicineTest
    {
        [TestMethod]
        public void SportInfoTest()
        {
            var sportRank = SportRank.First;
            var kind = "Футбол";

            var sportInfo = new SportInfo(sportRank, kind);

            Assert.AreEqual(sportRank, sportInfo.Rank);
            Assert.AreEqual(kind, sportInfo.Kind);
        }

        [TestMethod]
        public void PhysiologicalCharacteristicsTest()
        {
            var height = 180;
            var weight = 80;
            var headSize = 59;
            var clothingSize = "48/3";
            var shoesSize = 43;

            var physiologicalCharacteristics = new PhysiologicalCharacteristics(height, weight,
                                                   headSize, clothingSize, shoesSize);

            Assert.AreEqual(height, physiologicalCharacteristics.Height);
            Assert.AreEqual(weight, physiologicalCharacteristics.Weight);
            Assert.AreEqual(headSize, physiologicalCharacteristics.HeadSize);
            Assert.AreEqual(clothingSize, physiologicalCharacteristics.ClothingSize);
            Assert.AreEqual(shoesSize, physiologicalCharacteristics.ShoesSize);
        }

        [TestMethod]
        public void HealthTest()
        {
            var medicineRank = MedicineRank.A4;
            var additionalRequirementsTableGraphs = "2,4,7-10";
            var diseaseArticles = "13д,64г";
            var vision = "1,0/1,0";
            var bloodType = BloodType.OnePlus;

            var health = new Health(medicineRank, additionalRequirementsTableGraphs,
                                    diseaseArticles, vision, bloodType);

            Assert.AreEqual(medicineRank, health.MedicineRank);
            Assert.AreEqual(additionalRequirementsTableGraphs, health.AdditionalRequirementsTableGraphs);
            Assert.AreEqual(diseaseArticles, health.DiseaseArticles);
            Assert.AreEqual(vision, health.Vision);
            Assert.AreEqual(bloodType, health.BloodType);
        }

        [TestMethod]
        public void MedicineInfoTest()
        {
            var medicineRank = MedicineRank.A4;
            var additionalRequirementsTableGraphs = "2,4,7-10";
            var diseaseArticles = "13д,64г";
            var vision = "1,0/1,0";
            var bloodType = BloodType.OnePlus;
            var health = new Health(medicineRank, additionalRequirementsTableGraphs,
                                    diseaseArticles, vision, bloodType);

            var height = 180;
            var weight = 80;
            var headSize = 59;
            var clothingSize = "48/3";
            var shoesSize = 43;
            var physiologicalCharacteristics = new PhysiologicalCharacteristics(height, weight,
                                                   headSize, clothingSize, shoesSize);

            var sportRank = SportRank.First;
            var kind = "Футбол";
            var sportInfo = new SportInfo(sportRank, kind);

            var medicineInfo = new MedicineInfo(health, physiologicalCharacteristics, sportInfo);

            Assert.AreEqual(medicineRank, medicineInfo.Health.MedicineRank);
            Assert.AreEqual(additionalRequirementsTableGraphs, medicineInfo.Health.AdditionalRequirementsTableGraphs);
            Assert.AreEqual(diseaseArticles, medicineInfo.Health.DiseaseArticles);
            Assert.AreEqual(vision, medicineInfo.Health.Vision);
            Assert.AreEqual(bloodType, medicineInfo.Health.BloodType);

            Assert.AreEqual(height, medicineInfo.PhysiologicalCharacteristics.Height);
            Assert.AreEqual(weight, medicineInfo.PhysiologicalCharacteristics.Weight);
            Assert.AreEqual(headSize, medicineInfo.PhysiologicalCharacteristics.HeadSize);
            Assert.AreEqual(clothingSize, medicineInfo.PhysiologicalCharacteristics.ClothingSize);
            Assert.AreEqual(shoesSize, medicineInfo.PhysiologicalCharacteristics.ShoesSize);

            Assert.AreEqual(sportRank, medicineInfo.SportInfo.Rank);
            Assert.AreEqual(kind, medicineInfo.SportInfo.Kind);
        }
    }
}
