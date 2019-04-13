using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PupaParserComeback.Domain.DomainModels;
using PupaParserComeback.TestData;
using System.IO;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.Linq;
using PupaParserComeback.Domain.DomainModels.Passport;
using PupaParserComeback.Domain.ExtensionMethods.EnumExtensions;
using PupaParserComeback.Export.Decorators;
using PupaParserComeback.Domain.Constants;

namespace PupaParserComeback.Export.Test
{
    [TestClass]
    public class ExportTest
    {
        private const string TemplatesDir = "Templates";
        private const string ABPatternFileName = "ABPattern.xls";
        private static string _abTemplateFilePath;

        private const string TestEnvironmentName = "TestEnvironment";
        private string _excelFilePath;

        private string _photoExtension = ".jpg";

        [TestInitialize]
        public void Initialize()
        {
            var curDir = Directory.GetCurrentDirectory();
            var binPath = Directory.GetParent(curDir).FullName;
            var projPath = Directory.GetParent(binPath).FullName;

            _abTemplateFilePath = Path.Combine(projPath, TemplatesDir, ABPatternFileName);
            _excelFilePath = Path.Combine(projPath, TestEnvironmentName, $"{Guid.NewGuid()}.xls");
        }

        [TestMethod]
        public void ExportRecruitToExcelTest()
        {
            var recruitInfo = MemoryData.Build(1, 1, _photoExtension);

            var recruitExporter = new RecruitExcelExporter(_abTemplateFilePath, _excelFilePath);
            
            recruitExporter.ExportRecruitInfoesToExcel(recruitInfo);
            
            Excel.Application excel = null;
            Excel.Workbooks workbooks = null;
            Excel.Workbook workbook = null;
            Excel.Sheets worksheets = null;
            Excel.Worksheet worksheet = null;
            Excel.Range range = null;

            try
            {
                var recruitWorksheetIndex = 2;

                excel = new Excel.Application();
                excel.Visible = false;

                workbooks = excel.Workbooks;
                workbook = workbooks.Open(_excelFilePath);
                worksheets = workbook.Worksheets;
                worksheet = worksheets[recruitWorksheetIndex];
                range = worksheet.Cells;

                CheckRecruitInfo(recruitInfo, range, columnIdx: 2);

                workbook.Close();
                excel.Quit();
            }
            finally
            {
                if (range != null)
                {
                    Marshal.ReleaseComObject(range);
                }

                if (worksheet != null)
                {
                    Marshal.ReleaseComObject(worksheet);
                }

                if (worksheets != null)
                {
                    Marshal.ReleaseComObject(worksheets);
                }

                if (workbook != null)
                {
                    Marshal.ReleaseComObject(workbook);
                }

                if (workbooks != null)
                {
                    Marshal.ReleaseComObject(workbooks);
                }

                if (excel != null)
                {
                    Marshal.ReleaseComObject(excel);
                }
            }
        }

        [TestMethod]
        public void ExportTwoRecruitsToExcelTest()
        {
            var recruitInfo1 = MemoryData.Build(1, 1, _photoExtension);
            var recruitInfo2 = MemoryData.Build(2, 2, _photoExtension);

            var recruitExporter = new RecruitExcelExporter(_abTemplateFilePath, _excelFilePath);

            recruitExporter.ExportRecruitInfoesToExcel(recruitInfo1, recruitInfo2);

            Excel.Application excel = null;
            Excel.Workbooks workbooks = null;
            Excel.Workbook workbook = null;
            Excel.Sheets worksheets = null;
            Excel.Worksheet worksheet = null;
            Excel.Range range = null;

            try
            {
                var recruitWorksheetIndex = 2;

                excel = new Excel.Application();
                excel.Visible = false;

                workbooks = excel.Workbooks;
                workbook = workbooks.Open(_excelFilePath);
                worksheets = workbook.Worksheets;
                worksheet = worksheets[recruitWorksheetIndex];
                range = worksheet.Cells;

                CheckRecruitInfo(recruitInfo1, range, columnIdx: 2);
                CheckRecruitInfo(recruitInfo2, range, columnIdx: 3);

                workbook.Close();
                excel.Quit();
            }
            finally
            {
                if (range != null)
                {
                    Marshal.ReleaseComObject(range);
                }

                if (worksheet != null)
                {
                    Marshal.ReleaseComObject(worksheet);
                }

                if (worksheets != null)
                {
                    Marshal.ReleaseComObject(worksheets);
                }

                if (workbook != null)
                {
                    Marshal.ReleaseComObject(workbook);
                }

                if (workbooks != null)
                {
                    Marshal.ReleaseComObject(workbooks);
                }

                if (excel != null)
                {
                    Marshal.ReleaseComObject(excel);
                }
            }
        }

        private void CheckRecruitInfo(RecruitInfo recruitInfo, Excel.Range range, int columnIdx)
        {
            var rowRange = new Enumerable<int>(Enumerable.Range(RecruitExcelExporter.MinRowIdx, RecruitExcelExporter.MaxRowIdx));
            var keyIdx = 1;

            var rowIdx = rowRange.Next();
            Assert.AreEqual("Отдел", range[rowIdx, keyIdx].Value);
            Assert.AreEqual(recruitInfo.ServiceInfo.RegionalCollectionPoint, range[rowIdx, columnIdx].Value);

            rowIdx = rowRange.Next();
            Assert.AreEqual("Фамилия", range[rowIdx, keyIdx].Value);
            Assert.AreEqual(recruitInfo.Envelope.PassportInfo.PersonInfo.FullName.Surname, range[rowIdx, columnIdx].Value);

            rowIdx = rowRange.Next();
            Assert.AreEqual("Имя", range[rowIdx, keyIdx].Value);
            Assert.AreEqual(recruitInfo.Envelope.PassportInfo.PersonInfo.FullName.Name, range[rowIdx, columnIdx].Value);

            rowIdx = rowRange.Next();
            Assert.AreEqual("Отчество", range[rowIdx, keyIdx].Value);
            Assert.AreEqual(recruitInfo.Envelope.PassportInfo.PersonInfo.FullName.Patronymic, range[rowIdx, columnIdx].Value);

            rowIdx = rowRange.Next();
            Assert.AreEqual("Дата рожд", range[rowIdx, keyIdx].Value);
            var birthDate = recruitInfo.Envelope.PassportInfo.PersonInfo.BirthInfo.Date.ToString(DateConstants.RecruitDateFormat);
            Assert.AreEqual(birthDate, range[rowIdx, columnIdx].Value);

            rowIdx = rowRange.Next();
            Assert.AreEqual("Личный номер", range[rowIdx, keyIdx].Value);
            Assert.AreEqual(recruitInfo.Envelope.MilitaryInfo.PersonalNumber.Value, range[rowIdx, columnIdx].Value);

            rowIdx = rowRange.Next();
            Assert.AreEqual("Воен билет", range[rowIdx, keyIdx].Value);
            Assert.AreEqual(recruitInfo.Envelope.MilitaryInfo.Billet.BilletNumber.Value, range[rowIdx, columnIdx].Value);

            rowIdx = rowRange.Next();
            Assert.AreEqual("Паспорт", range[rowIdx, keyIdx].Value);
            Assert.AreEqual(recruitInfo.Envelope.PassportInfo.Code.Value, range[rowIdx, columnIdx].Value);

            rowIdx = rowRange.Next();
            Assert.AreEqual("Дата выдачи", range[rowIdx, keyIdx].Value);
            var passportIssueDate = recruitInfo.Envelope.PassportInfo.IssueInfo.IssueDate.ToString(DateConstants.RecruitDateFormat);
            Assert.AreEqual(passportIssueDate, range[rowIdx, columnIdx].Value);

            rowIdx = rowRange.Next();
            Assert.AreEqual("Кем выдан", range[rowIdx, keyIdx].Value);
            Assert.AreEqual(recruitInfo.Envelope.PassportInfo.IssueInfo.IssueBy, range[rowIdx, columnIdx].Value);

            rowIdx = rowRange.Next();
            Assert.AreEqual("Специальность", range[rowIdx, keyIdx].Value);
            Assert.AreEqual(recruitInfo.Envelope.MilitaryInfo.Speciality, range[rowIdx, columnIdx].Value);

            rowIdx = rowRange.Next();
            Assert.AreEqual("Номер удостов", range[rowIdx, keyIdx].Value);
            Assert.AreEqual(recruitInfo.Envelope.DriverInfo.Code.Value, range[rowIdx, columnIdx].Value);

            rowIdx = rowRange.Next();
            Assert.AreEqual("Режим", range[rowIdx, keyIdx].Value);
            Assert.AreEqual(recruitInfo.Envelope.MilitaryInfo.TeamMode, range[rowIdx, columnIdx].Value);

            rowIdx = rowRange.Next();
            Assert.AreEqual("Место рожд", range[rowIdx, keyIdx].Value);
            Assert.AreEqual(recruitInfo.Envelope.PassportInfo.PersonInfo.BirthInfo.Place, range[rowIdx, columnIdx].Value);

            rowIdx = rowRange.Next();
            Assert.AreEqual("НПУ", range[rowIdx, keyIdx].Value);
            var nps = recruitInfo.Envelope.MilitaryInfo.ProficiencyCard.NervouslyPsychologicalStability
                .ToNervouslyPsychologicalStatusString().Substring(0, 3) + ".";

            Assert.AreEqual(nps, range[rowIdx, columnIdx].Value);

            rowIdx = rowRange.Next();
            Assert.AreEqual("Профпригодность", range[rowIdx, keyIdx].Value);
            var proficiency = recruitInfo.Envelope.MilitaryInfo.ProficiencyCard.ProficiencyCategory.ToProficiencyCategoryString();
            Assert.AreEqual(proficiency, range[rowIdx, columnIdx].Value);

            rowIdx = rowRange.Next();
            Assert.AreEqual("Кат годности", range[rowIdx, keyIdx].Value);
            var medicineRank = recruitInfo.MedicineInfo.Health.MedicineRank.ToMedicineRankString();
            Assert.AreEqual(medicineRank, range[rowIdx, columnIdx].Value);

            rowIdx = rowRange.Next();
            Assert.AreEqual("Графы", range[rowIdx, keyIdx].Value);
            Assert.AreEqual(recruitInfo.MedicineInfo.Health.AdditionalRequirementsTableGraphs, range[rowIdx, columnIdx].Value);

            rowIdx = rowRange.Next();
            Assert.AreEqual("Зрение без кор", range[rowIdx, keyIdx].Value);
            Assert.AreEqual(recruitInfo.MedicineInfo.Health.Vision, range[rowIdx, columnIdx].Value);

            rowIdx = rowRange.Next();
            Assert.AreEqual("Зрение с кор", range[rowIdx, keyIdx].Value);
            Assert.AreEqual(recruitInfo.MedicineInfo.Health.Vision, range[rowIdx, columnIdx].Value);

            rowIdx = rowRange.Next();
            Assert.AreEqual("рост", range[rowIdx, keyIdx].Value);
            var height = recruitInfo.MedicineInfo.PhysiologicalCharacteristics.Height.ToString();
            Assert.AreEqual(height, range[rowIdx, columnIdx].Value);

            rowIdx = rowRange.Next();
            Assert.AreEqual("вес", range[rowIdx, keyIdx].Value);
            var weight = recruitInfo.MedicineInfo.PhysiologicalCharacteristics.Weight.ToString();
            Assert.AreEqual(weight, range[rowIdx, columnIdx].Value);

            rowIdx = rowRange.Next();
            Assert.AreEqual("Статьи", range[rowIdx, keyIdx].Value);
            Assert.AreEqual(recruitInfo.MedicineInfo.Health.DiseaseArticles, range[rowIdx, columnIdx].Value);

            rowIdx = rowRange.Next();
            Assert.AreEqual("Дата приб", range[rowIdx, keyIdx].Value);
            var conscriptionDate = recruitInfo.ServiceInfo.ConscriptionDate.ToString("D");
            Assert.AreEqual(conscriptionDate, range[rowIdx, columnIdx].Value);

            if (recruitInfo.Envelope.MilitaryInfo.Billet.IsHaveSecretAccess)
            {
                rowIdx = rowRange.Next();
                Assert.AreEqual("Форма допуска", range[rowIdx, keyIdx].Value);
                var form = recruitInfo.Envelope.MilitaryInfo.Billet.SecretAccess.AccessForm.ToAccessFormString();
                Assert.AreEqual(form, range[rowIdx, columnIdx].Value);

                rowIdx = rowRange.Next();
                Assert.AreEqual("Номер допуска", range[rowIdx, keyIdx].Value);
                var number = recruitInfo.Envelope.MilitaryInfo.Billet.SecretAccess.SecretAccessNumber;
                Assert.AreEqual(number, range[rowIdx, columnIdx].Value);

                rowIdx = rowRange.Next();
                Assert.AreEqual("Дата допуска", range[rowIdx, keyIdx].Value);
                var date = recruitInfo.Envelope.MilitaryInfo.Billet.SecretAccess.IssueDate.ToString(DateConstants.RecruitDateFormat);
                Assert.AreEqual(date, range[rowIdx, columnIdx].Value);
            }

            rowIdx = recruitInfo.Envelope.MilitaryInfo.Billet.IsHaveSecretAccess ? rowRange.Next() : rowRange.Next(3);
            Assert.AreEqual("Образование", range[rowIdx, keyIdx].Value);
            var education = recruitInfo.Envelope.CivilInfo.Education.ToEducationStatusString();
            Assert.AreEqual(education, range[rowIdx, columnIdx].Value);

            rowIdx = rowRange.Next();
            Assert.AreEqual("разряд", range[rowIdx, keyIdx].Value);
            var sportRank = recruitInfo.MedicineInfo.SportInfo.Rank.ToSportRankString();

            Assert.AreEqual(sportRank, range[rowIdx, columnIdx].Value);

            rowIdx = rowRange.Next();
            Assert.AreEqual("судимость", range[rowIdx, keyIdx].Value);
            var criminal = recruitInfo.CriminalInfo.CriminalStatus.ToCriminalStatusString();
            Assert.AreEqual(criminal, range[rowIdx, columnIdx].Value);

            rowIdx = rowRange.Next();
            Assert.AreEqual("Семья", range[rowIdx, keyIdx].Value);
            var parentFamily = recruitInfo.Envelope.FamilyInfo.ParentFamilyStatus.ToParentFamilyStatusString();
            Assert.AreEqual(parentFamily, range[rowIdx, columnIdx].Value);

            rowIdx = rowRange.Next();
            Assert.AreEqual("На учете", range[rowIdx, keyIdx].Value);
            var register = recruitInfo.CriminalInfo.RegisterStatus.ToRegisterStatusString();
            Assert.AreEqual(register, range[rowIdx, columnIdx].Value);

            rowIdx = rowRange.Next();
            Assert.AreEqual("Семейное положение", range[rowIdx, keyIdx].Value);
            var selfFamily = recruitInfo.Envelope.PassportInfo.FamilyInfo.FamilyStatus.ToFamilyStatusString();
            Assert.AreEqual(selfFamily, range[rowIdx, columnIdx].Value);

            rowIdx = rowRange.Next();
            Assert.AreEqual("Наличие ребенка", range[rowIdx, keyIdx].Value);
            var isHaveBaby = recruitInfo.Envelope.PassportInfo.FamilyInfo.IsHaveBaby
                ? PassportFamilyInfo.HaveBaby
                : PassportFamilyInfo.NotHaveBaby;
            Assert.AreEqual(isHaveBaby, range[rowIdx, columnIdx].Value);

            rowIdx = rowRange.Next();
            Assert.AreEqual("Гражданская спец", range[rowIdx, keyIdx].Value);
            Assert.AreEqual(recruitInfo.Envelope.CivilInfo.Profession, range[rowIdx, columnIdx].Value);

            rowIdx = rowRange.Next();
            Assert.AreEqual("Род занятий", range[rowIdx, keyIdx].Value);
            var occupation = recruitInfo.Envelope.CivilInfo.Occupation.ToOccupationStatusString();
            Assert.AreEqual(occupation, range[rowIdx, columnIdx].Value);

            rowIdx = rowRange.Next();
            Assert.AreEqual("Головной убор", range[rowIdx, keyIdx].Value);
            var head = recruitInfo.MedicineInfo.PhysiologicalCharacteristics.HeadSize.ToString();
            Assert.AreEqual(head, range[rowIdx, columnIdx].Value);

            rowIdx = rowRange.Next();
            Assert.AreEqual("Обхват рост", range[rowIdx, keyIdx].Value);
            var clothing = recruitInfo.MedicineInfo.PhysiologicalCharacteristics.ClothingSize;
            Assert.AreEqual(clothing, range[rowIdx, columnIdx].Value);

            rowIdx = rowRange.Next();
            Assert.AreEqual("Обувь", range[rowIdx, keyIdx].Value);
            var shoes = recruitInfo.MedicineInfo.PhysiologicalCharacteristics.ShoesSize.ToString();
            Assert.AreEqual(shoes, range[rowIdx, columnIdx].Value);

            rowIdx = rowRange.Next();
            Assert.AreEqual("FORM ID", range[rowIdx, keyIdx].Value);
            var formId = recruitInfo.ServiceInfo.FirebirdId.ToString();
            Assert.AreEqual(formId, range[rowIdx, columnIdx].Value);
        }

        [TestCleanup]
        public void Cleanup()
        {
            File.Delete(_excelFilePath);
        }
    }
}
