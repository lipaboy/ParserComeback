using PupaParserComeback.Domain.Constants;
using PupaParserComeback.Domain.Enums;
using PupaParserComeback.Domain.Indexes;
using PupaParserComeback.Import.Constants;
using PupaParserComeback.Import.ImportSources;
using PupaParserComeback.Presentation.Interfaces;
using PupaParserComeback.Presentation.Models;
using PupaParserComeback.Presentation.Models.CardGroups;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PupaParserComeback.Import
{
    public class PupaRecruitImporter : IRecruitImporter
    {
        public const int RecruitWordsCount = 64;

        private string _importDirectoryPath;
        public string ImportDirectoryPath
        {
            get { return _importDirectoryPath; }
        }

        private string _personalPhotoDirectoryPath;
        public string PersonalPhotoDirectoryPath
        {
            get { return _personalPhotoDirectoryPath; }
        }

        public PupaRecruitImporter(string importDirectoryPath, string personalPhotoDirectoryPath)
        {
            if (string.IsNullOrWhiteSpace(importDirectoryPath))
            {
                throw new ArgumentNullException(nameof(importDirectoryPath));
            }

            if (string.IsNullOrWhiteSpace(personalPhotoDirectoryPath))
            {
                throw new ArgumentNullException(nameof(personalPhotoDirectoryPath));
            }

            _importDirectoryPath = importDirectoryPath;
            _personalPhotoDirectoryPath = personalPhotoDirectoryPath;
        }

        public void ChangeImportDirectoryPath(string importDirectoryPath)
        {
            if (string.IsNullOrWhiteSpace(importDirectoryPath))
            {
                throw new ArgumentNullException(nameof(importDirectoryPath));
            }

            if (!Directory.Exists(importDirectoryPath))
            {
                throw new ArgumentException(nameof(importDirectoryPath));
            }

            _importDirectoryPath = importDirectoryPath;
        }

        public void ChangePersonalPhotoDirectoryPath(string personalPhotoDirectoryPath)
        {
            if (string.IsNullOrWhiteSpace(personalPhotoDirectoryPath))
            {
                throw new ArgumentNullException(nameof(personalPhotoDirectoryPath));
            }

            _personalPhotoDirectoryPath = personalPhotoDirectoryPath;
        }

        public void CopyPersonalPhotos(DateTime? conscriptionDate = null,
            string selectedRegionalCollectionPoint = null)
        {
            var conscriptionDateDirPaths = GetConscriptionDateDirPaths(conscriptionDate);

            foreach (var conscriptionDateDirPath in conscriptionDateDirPaths)
            {
                var rcpDirPaths = GetRegionalCollectionPointDirPaths(conscriptionDateDirPath, selectedRegionalCollectionPoint);

                foreach (var rcpDirPath in rcpDirPaths)
                {
                    foreach (var imageFilePath in Directory.EnumerateFiles(rcpDirPath))
                    {
                        if (Path.GetExtension(imageFilePath) == Extensions.PhotoExtension)
                        {
                            var src = imageFilePath;
                            var dst = Path.Combine(PersonalPhotoDirectoryPath, Path.GetFileName(imageFilePath));

                            if (!File.Exists(dst))
                            {
                                File.Copy(src, dst);
                            }
                        }
                    }
                }
            }
        }

        public IEnumerable<RecruitShortUIModel> ImportRecruitShortUIModels(DateTime? conscriptionDate = null,
            string selectedRegionalCollectionPoint = null)
        {
            var recruitShortUIModels = new List<RecruitShortUIModel>();

            var conscriptionDateDirPaths = GetConscriptionDateDirPaths(conscriptionDate);

            foreach (var conscriptionDateDirPath in conscriptionDateDirPaths)
            {
                var rcpDirPaths = GetRegionalCollectionPointDirPaths(conscriptionDateDirPath, selectedRegionalCollectionPoint);

                foreach (var rcpDirPath in rcpDirPaths)
                {
                    foreach (var plainFilePath in Directory.EnumerateFiles(rcpDirPath))
                    {
                        if (Path.GetExtension(plainFilePath) == Extensions.PlainExtension)
                        {
                            var recruitInfoesFromFile = GetRecruitShortUIModels(plainFilePath);
                            recruitShortUIModels.AddRange(recruitInfoesFromFile);
                        }
                    }
                }
            }

            return recruitShortUIModels;
        }

        private IEnumerable<string> GetConscriptionDateDirPaths(DateTime? conscriptionDate)
        {
            var conscriptionDateDirPaths = Directory.EnumerateDirectories(_importDirectoryPath);

            if (conscriptionDate.HasValue)
            {
                var conscriptionDateDirPath = Path.Combine(_importDirectoryPath, conscriptionDate.Value.ToString(DateConstants.RecruitDateFormat));
                conscriptionDateDirPaths = new List<string>() { conscriptionDateDirPath };
            }

            return conscriptionDateDirPaths;
        }

        private IEnumerable<string> GetRegionalCollectionPointDirPaths(string conscriptionDateDirPath, string selectedRegionalCollectionPoint)
        {
            var rcpDirPaths = Directory.EnumerateDirectories(conscriptionDateDirPath);

            if (!string.IsNullOrWhiteSpace(selectedRegionalCollectionPoint))
            {
                var rcpDirPath = Path.Combine(conscriptionDateDirPath, selectedRegionalCollectionPoint);
                rcpDirPaths = new List<string>() { rcpDirPath };
            }

            return rcpDirPaths;
        }

        private IEnumerable<RecruitShortUIModel> GetRecruitShortUIModels(string plainFilePath)
        {
            var recruitInfoes = new List<RecruitShortUIModel>();

            var importFileReader = new ImportFileReader(plainFilePath);
            var words = importFileReader.ReadAllWords();
            
            var firstRecruitInfo = CreateRecruitShortUIModel(plainFilePath, words, 
                isFirstRecruit: true);

            recruitInfoes.Add(firstRecruitInfo);
            
            if (IsExistsSecondRecruit(words[64]))
            {
                var secondRecruitInfo = CreateRecruitShortUIModel(plainFilePath, words, 
                    isFirstRecruit: false);

                recruitInfoes.Add(secondRecruitInfo);
            }

            return recruitInfoes;
        }

        private RecruitShortUIModel CreateRecruitShortUIModel(string plainFilePath, 
            string[] words, bool isFirstRecruit)
        {
            var recruitWords = GetRecruitWords(words, isFirstRecruit);
            
            DateTime outBirthDate;
            DateTime? birthDate = DateTime.TryParse(recruitWords[4], out outBirthDate)
                ? outBirthDate
                : (DateTime?)null;

            var rcp = GetRegionalCollectionPoint(words, isFirstRecruit);
            var conscriptionDate = GetConscriptionDate(plainFilePath);

            return new RecruitShortUIModel(surname: recruitWords[1],
                name: recruitWords[2],
                patronymic: recruitWords[3],
                passportCode: recruitWords[7],
                birthDate: birthDate,
                regionalCollectionPoint: rcp,
                conscriptionDate: conscriptionDate,
                storage: Storage.File,
                filePath: plainFilePath);
        }

        private DateTime GetConscriptionDate(string plainFilePath)
        {
            var regionalCollectionPointPath = Directory.GetParent(plainFilePath).FullName;
            var conscriptionDateDirectoryName = Directory.GetParent(regionalCollectionPointPath).Name;

            DateTime date;
            return DateTime.TryParse(conscriptionDateDirectoryName, out date)
                ? date
                : DateTime.Now;
        }

        public RecruitCardGroup ImportRecruitCardGroup(RecruitShortUIModel recruitShortUIModel)
        {
            if (recruitShortUIModel == null)
            {
                throw new ArgumentNullException(nameof(recruitShortUIModel));
            }

            var importFileReader = new ImportFileReader(recruitShortUIModel.FilePath);
            var words = importFileReader.ReadAllWords();

            RecruitIndex idxFromRecruitShortUIModel = new RecruitIndex(recruitShortUIModel.Surname,
                recruitShortUIModel.Name,
                recruitShortUIModel.Patronymic,
                recruitShortUIModel.RegionalCollectionPoint);

            RecruitIndex firstIdxFromImportedFile = GetFirstRecruitIdxFromImportedFile(words);
            RecruitIndex secondIdxFromImportedFile = GetSecondRecruitIdxFromImportedFile(words);

            bool isFirstRecruit = idxFromRecruitShortUIModel == firstIdxFromImportedFile;
            bool isSecondRecruit = idxFromRecruitShortUIModel == secondIdxFromImportedFile;

            if (isFirstRecruit && isSecondRecruit)
            {
                var index = idxFromRecruitShortUIModel.ToString();
                var msg = $"File with path: {recruitShortUIModel.FilePath} have not recruit: {index}";
                throw new ArgumentException(msg);
            }

            return CreateCardGroup(words, isFirstRecruit);
        }

        private RecruitCardGroup CreateCardGroup(string[] words, bool isFirstRecruit)
        {
            var rcp = GetRegionalCollectionPoint(words, isFirstRecruit);
            var recruitWords = GetRecruitWords(words, isFirstRecruit);

            var recruitCardGroupBuilder = new RecruitCardGroupBuilder(_personalPhotoDirectoryPath,
                rcp, recruitWords);

            return recruitCardGroupBuilder.Build();
        }

        private string[] GetRecruitWords(string[] words, bool isFirstRecruit)
        {
            var firstRecruitWordsCount = RecruitWordsCount;
            var secondRecruitWordsCount = words.Length - firstRecruitWordsCount;

            return isFirstRecruit
                ? words.Take(firstRecruitWordsCount).ToArray()
                : words.Skip(firstRecruitWordsCount - 1).Take(secondRecruitWordsCount + 1).ToArray();
        }

        private RecruitIndex GetFirstRecruitIdxFromImportedFile(string[] words)
        {
            RecruitIndex firstIdxFromImportedFile = null;

            if (IsExistsSecondRecruit(words[1]))
            {
                firstIdxFromImportedFile = new RecruitIndex(surname: words[1],
                    name: words[2],
                    patronymic: words[3],
                    regionalCollectionPoint: GetRegionalCollectionPoint(words, isFirstRecruit: true));
            }

            return firstIdxFromImportedFile;
        }

        private RecruitIndex GetSecondRecruitIdxFromImportedFile(string[] words)
        {
            RecruitIndex secondIdxFromImportedFile = null;

            if (IsExistsSecondRecruit(words[64]))
            {
                secondIdxFromImportedFile = new RecruitIndex(surname: words[64],
                    name: words[65],
                    patronymic: words[66],
                    regionalCollectionPoint: GetRegionalCollectionPoint(words, isFirstRecruit: false));
            }

            return secondIdxFromImportedFile;
        }

        private bool IsExistsSecondRecruit(string word)
        {
            return !string.IsNullOrWhiteSpace(word);
        }

        private string GetRegionalCollectionPoint(string[] words, bool isFirstRecruit)
        {
            return isFirstRecruit
                ? words[0]
                : words[63].Split(new string[] { Environment.NewLine },
                    StringSplitOptions.None)[1];
        }

        public async Task<IEnumerable<RecruitShortUIModel>> ImportRecruitShortUIModelsAsync(DateTime? conscriptionDate = null,
            string selectedRegionalCollectionPoint = null)
        {
            return await Task.Run(() =>
            {
                return ImportRecruitShortUIModels(conscriptionDate, selectedRegionalCollectionPoint);
            });
        }

        public async Task CopyPersonalPhotosAsync(DateTime? conscriptionDate = null,
            string selectedRegionalCollectionPoint = null)
        {
            await Task.Run(() => CopyPersonalPhotos(conscriptionDate, selectedRegionalCollectionPoint));
        }
    }
}
