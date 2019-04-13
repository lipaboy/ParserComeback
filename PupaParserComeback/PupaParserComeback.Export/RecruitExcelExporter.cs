using PupaParserComeback.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using PupaParserComeback.Domain.DomainModels.Passport;
using PupaParserComeback.Domain.ExtensionMethods.EnumExtensions;
using Excel = Microsoft.Office.Interop.Excel;
using PupaParserComeback.Export.Models;
using PupaParserComeback.Presentation.Interfaces;
using PupaParserComeback.Presentation.Models;
using PupaParserComeback.Export.Decorators;
using PupaParserComeback.Domain.Constants;

namespace PupaParserComeback.Export
{
    public class RecruitExcelExporter : IRecruitExcelExporter
    {
        public const int MinRowIdx = 1;
        public const int MaxRowIdx = 40;

        private readonly string _templateFilePath;
        private readonly string _filePath;
        private readonly bool _isOpenFile;

        public RecruitExcelExporter(string templateFilePath, string filePath, 
            bool isOpenFile = false)
        {
            if (string.IsNullOrWhiteSpace(templateFilePath))
            {
                throw new ArgumentNullException(nameof(templateFilePath));
            }

            if (!File.Exists(templateFilePath))
            {
                throw new ArgumentException(nameof(templateFilePath));
            }

            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }
            
            _templateFilePath = templateFilePath;
            _filePath = filePath;
            _isOpenFile = isOpenFile;
        }

        public void ExportRecruitInfoesToExcel(RecruitInfo firstRecruit, 
            RecruitInfo secondRecruit = null)
        {
            CreateExportFileFromTemplate();

            Excel.Application application = null;
            Excel.Workbooks workbooks = null;
            Excel.Workbook workbook = null;
            Excel.Sheets worksheets = null;
            Excel.Worksheet worksheet = null;
            Excel.Range range = null;

            try
            {
                var recruitWorksheetIndex = 2;
                var startColumnIdx = 2;
                var endColumnIdx = 3;

                application = new Excel.Application();
                application.Visible = _isOpenFile;
                application.DisplayAlerts = false;

                workbooks = application.Workbooks;
                workbook = workbooks.Open(_filePath);
                worksheets = workbook.Worksheets;
                worksheet = worksheets[recruitWorksheetIndex];
                range = worksheet.Cells;

                FillRecruitInfo(firstRecruit, range, startColumnIdx);

                if (secondRecruit != null)
                {
                    FillRecruitInfo(secondRecruit, range, endColumnIdx);
                }

                if (_isOpenFile)
                {
                    workbook.Save();
                }
                else
                {
                    workbook.Close(SaveChanges: true);
                    application.Quit();
                }
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

                if (application != null)
                {
                    Marshal.ReleaseComObject(application);
                }
            }
        }

        private void FillRecruitInfo(RecruitInfo recruitInfo, Excel.Range range, int columnIdx)
        {
            var rowIndexes = new Enumerable<int>(Enumerable.Range(MinRowIdx, MaxRowIdx));

            // don't use:
            // range[rowIdx, valueIdx].Value = "Test";
            // because it hold excel proc

            range.set_Item(rowIndexes.Next(), columnIdx, recruitInfo.ServiceInfo.RegionalCollectionPoint);
            range.set_Item(rowIndexes.Next(), columnIdx, recruitInfo.Envelope.PassportInfo.PersonInfo.FullName.Surname);
            range.set_Item(rowIndexes.Next(), columnIdx, recruitInfo.Envelope.PassportInfo.PersonInfo.FullName.Name);
            range.set_Item(rowIndexes.Next(), columnIdx, recruitInfo.Envelope.PassportInfo.PersonInfo.FullName.Patronymic);

            range.set_Item(rowIndexes.Next(), columnIdx, recruitInfo.Envelope.PassportInfo.PersonInfo.BirthInfo.Date
                .ToString(DateConstants.RecruitDateFormat));

            range.set_Item(rowIndexes.Next(), columnIdx, recruitInfo.Envelope.MilitaryInfo.PersonalNumber.Value);
            range.set_Item(rowIndexes.Next(), columnIdx, recruitInfo.Envelope.MilitaryInfo.Billet.BilletNumber.Value);
            range.set_Item(rowIndexes.Next(), columnIdx, recruitInfo.Envelope.PassportInfo.Code.Value);
            range.set_Item(rowIndexes.Next(), columnIdx, recruitInfo.Envelope.PassportInfo.IssueInfo.IssueDate
                .ToString(DateConstants.RecruitDateFormat));

            range.set_Item(rowIndexes.Next(), columnIdx, recruitInfo.Envelope.PassportInfo.IssueInfo.IssueBy);
            range.set_Item(rowIndexes.Next(), columnIdx, recruitInfo.Envelope.MilitaryInfo.Speciality);

            if (recruitInfo.Envelope.IsDriver)
            {
                range.set_Item(rowIndexes.Next(), columnIdx, recruitInfo.Envelope.DriverInfo.Code.Value);
            }
            else
            {
                rowIndexes.Next();
            }

            range.set_Item(rowIndexes.Next(), columnIdx, recruitInfo.Envelope.MilitaryInfo.TeamMode);
            range.set_Item(rowIndexes.Next(), columnIdx, recruitInfo.Envelope.PassportInfo.PersonInfo.BirthInfo.Place);

            var nps = recruitInfo.Envelope.MilitaryInfo.ProficiencyCard.NervouslyPsychologicalStability
                .ToNervouslyPsychologicalStatusString().Substring(0, 3) + ".";
            range.set_Item(rowIndexes.Next(), columnIdx, nps);

            range.set_Item(rowIndexes.Next(), columnIdx, recruitInfo.Envelope.MilitaryInfo.ProficiencyCard.ProficiencyCategory
                .ToProficiencyCategoryString());

            range.set_Item(rowIndexes.Next(), columnIdx, recruitInfo.MedicineInfo.Health.MedicineRank.ToMedicineRankString());
            range.set_Item(rowIndexes.Next(), columnIdx, recruitInfo.MedicineInfo.Health.AdditionalRequirementsTableGraphs);

            // vision without correction
            range.set_Item(rowIndexes.Next(), columnIdx, recruitInfo.MedicineInfo.Health.Vision);
            // vision with correction
            range.set_Item(rowIndexes.Next(), columnIdx, recruitInfo.MedicineInfo.Health.Vision);

            range.set_Item(rowIndexes.Next(), columnIdx, recruitInfo.MedicineInfo.PhysiologicalCharacteristics.Height.ToString());
            range.set_Item(rowIndexes.Next(), columnIdx, recruitInfo.MedicineInfo.PhysiologicalCharacteristics.Weight.ToString());
            range.set_Item(rowIndexes.Next(), columnIdx, recruitInfo.MedicineInfo.Health.DiseaseArticles);
            range.set_Item(rowIndexes.Next(), columnIdx, recruitInfo.ServiceInfo.ConscriptionDate.ToString("D"));

            if (recruitInfo.Envelope.MilitaryInfo.Billet.IsHaveSecretAccess)
            {
                range.set_Item(rowIndexes.Next(), columnIdx, recruitInfo.Envelope.MilitaryInfo.Billet.SecretAccess.AccessForm.ToAccessFormString());
                range.set_Item(rowIndexes.Next(), columnIdx, recruitInfo.Envelope.MilitaryInfo.Billet.SecretAccess.SecretAccessNumber);
                range.set_Item(rowIndexes.Next(), columnIdx, recruitInfo.Envelope.MilitaryInfo.Billet.SecretAccess.IssueDate
                    .ToString(DateConstants.RecruitDateFormat));
            }
            else
            {
                rowIndexes.Next(3);
            }

            range.set_Item(rowIndexes.Next(), columnIdx, recruitInfo.Envelope.CivilInfo.Education.ToEducationStatusString());
            range.set_Item(rowIndexes.Next(), columnIdx, recruitInfo.CriminalInfo.CriminalStatus.ToCriminalStatusString());
            range.set_Item(rowIndexes.Next(), columnIdx, recruitInfo.MedicineInfo.SportInfo.Rank.ToSportRankString());

            range.set_Item(rowIndexes.Next(), columnIdx, recruitInfo.Envelope.FamilyInfo.ParentFamilyStatus
                .ToParentFamilyStatusString());

            range.set_Item(rowIndexes.Next(), columnIdx, recruitInfo.CriminalInfo.RegisterStatus.ToRegisterStatusString());
            range.set_Item(rowIndexes.Next(), columnIdx, recruitInfo.Envelope.PassportInfo.FamilyInfo.FamilyStatus.ToFamilyStatusString());
            range.set_Item(rowIndexes.Next(), columnIdx, recruitInfo.Envelope.PassportInfo.FamilyInfo.IsHaveBaby
                ? PassportFamilyInfo.HaveBaby
                : PassportFamilyInfo.NotHaveBaby);

            range.set_Item(rowIndexes.Next(), columnIdx, recruitInfo.Envelope.CivilInfo.Profession);
            range.set_Item(rowIndexes.Next(), columnIdx, recruitInfo.Envelope.CivilInfo.Occupation.ToOccupationStatusString());
            range.set_Item(rowIndexes.Next(), columnIdx, recruitInfo.MedicineInfo.PhysiologicalCharacteristics.HeadSize.ToString());
            range.set_Item(rowIndexes.Next(), columnIdx, recruitInfo.MedicineInfo.PhysiologicalCharacteristics.ClothingSize);
            range.set_Item(rowIndexes.Next(), columnIdx, recruitInfo.MedicineInfo.PhysiologicalCharacteristics.ShoesSize.ToString());
            range.set_Item(rowIndexes.Next(), columnIdx, recruitInfo.ServiceInfo.FirebirdId.ToString());
        }
        
        public void ExportRecruitShortUIModelsToExcelTable(IEnumerable<RecruitShortUIModel> recruitShortUIModels,
            string regionalCollectionPoint, 
            string conscriptionDate)
        {
            CreateExportFileFromTemplate();

            var recruitExcelItems = recruitShortUIModels.Select(r =>
                         new RecruitExcelTableItem(r.Surname,
                             r.Name,
                             r.Patronymic,
                             r.BirthDate,
                             r.RegionalCollectionPoint,
                             r.FirebirdId.HasValue ? r.FirebirdId.Value.ToString() : string.Empty))
                         .OrderBy(r => r.FormId);

            var recruitWorksheetIndex = 1;

            Excel.Application application = null;
            Excel.Workbooks workbooks = null;
            Excel.Workbook workbook = null;
            Excel.Sheets worksheets = null;
            Excel.Worksheet worksheet = null;
            Excel.Range range = null;

            try
            {
                application = new Excel.Application();
                application.Visible = _isOpenFile;
                application.DisplayAlerts = false;

                workbooks = application.Workbooks;
                workbook = workbooks.Open(_filePath);
                worksheets = workbook.Worksheets;
                worksheet = worksheets[recruitWorksheetIndex];
                range = worksheet.Cells;

                FillTableHeader(range, regionalCollectionPoint, conscriptionDate);
                FillTableContent(range, recruitExcelItems);

                if (_isOpenFile)
                {
                    workbook.Save();
                }
                else
                {
                    workbook.Close(SaveChanges: true);
                    application.Quit();
                }
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

                if (application != null)
                {
                    Marshal.ReleaseComObject(application);
                }
            }
        }

        private void FillTableHeader(Excel.Range range,
            string regionalCollectionPoint,
            string conscriptionDate)
        {
            var header = $"{regionalCollectionPoint} {conscriptionDate}";
            range.set_Item(1, 2, header);
        }

        private void FillTableContent(Excel.Range range,
            IEnumerable<RecruitExcelTableItem> tableItems)
        {
            var contentStartRowIndex = 4;
            
            var rowRange = new Enumerable<int>(Enumerable.Range(
                start: contentStartRowIndex,
                count: tableItems.Count()));

            int idx = 0;
            var rowIdx = rowRange.Next();

            var numberColumnIdx = 1;
            var surnameColumnIdx = 2;
            var nameColumnIdx = 3;
            var patronymicColumnIdx = 4;
            var birthDateColumnIdx = 5;
            var rcpColumnIdx = 6;
            var formIdColumnIdx = 7;

            foreach (var tableItem in tableItems)
            {
                range.set_Item(rowIdx, numberColumnIdx, ++idx);
                range.set_Item(rowIdx, surnameColumnIdx, tableItem.Surname);
                range.set_Item(rowIdx, nameColumnIdx, tableItem.Name);
                range.set_Item(rowIdx, patronymicColumnIdx, tableItem.Patronymic);
                range.set_Item(rowIdx, birthDateColumnIdx, tableItem.BirthDate);
                range.set_Item(rowIdx, rcpColumnIdx, tableItem.RegionalCollectionPoint);
                range.set_Item(rowIdx, formIdColumnIdx, tableItem.FormId);

                rowIdx = rowRange.Next();
            }
        }
        
        private void CreateExportFileFromTemplate()
        {
            File.Copy(_templateFilePath, _filePath, overwrite: true);
        }
    }
}
