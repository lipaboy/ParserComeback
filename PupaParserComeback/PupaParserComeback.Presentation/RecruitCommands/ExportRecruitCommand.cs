using PupaParserComeback.Domain.DomainModels;
using PupaParserComeback.Domain.Interfaces;
using PupaParserComeback.Presentation.Abstract;
using PupaParserComeback.Presentation.Enums;
using PupaParserComeback.Presentation.Interfaces;
using PupaParserComeback.Presentation.RecruitCommands.Parameters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PupaParserComeback.Presentation.RecruitCommands
{
    public class ExportRecruitCommand : IParameterizedCommandAsync<ExportRecruitCommandParameters>
    {
        public const string CommandName = "Экспорт в алфавитку";
        public const string CommandSuccess = "Экспорт в алфавитку выполнен";

        private readonly IRecruitInfoRepository _recruitInfoRepository;
        private readonly IRecruitExcelExporterFactory _recruitExcelExporterFactory;

        private readonly string _exportTemplateFilePath;
        private readonly string _exportDirectoryPath;

        public ExportRecruitCommand(IRecruitInfoRepository recruitInfoRepository,
            IRecruitExcelExporterFactory recruitExcelExporterFactory,
            string exportTemplateFilePath,
            string exportDirectoryPath)
        {
            if (recruitInfoRepository == null)
            {
                throw new ArgumentNullException(nameof(recruitInfoRepository));
            }

            if (recruitExcelExporterFactory == null)
            {
                throw new ArgumentNullException(nameof(recruitExcelExporterFactory));
            }

            if (string.IsNullOrWhiteSpace(exportTemplateFilePath))
            {
                _exportTemplateFilePath = exportTemplateFilePath;
            }

            if (string.IsNullOrWhiteSpace(exportDirectoryPath))
            {
                _exportDirectoryPath = exportDirectoryPath;
            }

            _recruitInfoRepository = recruitInfoRepository;
            _recruitExcelExporterFactory = recruitExcelExporterFactory;

            _exportTemplateFilePath = exportTemplateFilePath;
            _exportDirectoryPath = exportDirectoryPath;
        }
        
        public async Task ExecuteAsync(ExportRecruitCommandParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }
            
            IEnumerable<RecruitInfo> recruitInfoes = _recruitInfoRepository.Get(parameters.RecruitIds);

            await Task.Run(() =>
            {
                var recruitCount = recruitInfoes.Count();

                for (int i = 0; i < recruitCount; i += 2)
                {
                    var firstRecruit = recruitInfoes.ElementAt(i);
                    var secondRecruit = recruitInfoes.ElementAtOrDefault(i + 1);

                    var recruitExporter = CreateRecruitExporter(parameters, 
                        firstRecruit, secondRecruit);

                    recruitExporter.ExportRecruitInfoesToExcel(firstRecruit, secondRecruit);
                }

                parameters.StateChanged.OnStateChanged(CommandSuccess, StateResult.Success);
            });
        }

        private IRecruitExcelExporter CreateRecruitExporter(ExportRecruitCommandParameters parameters,
            RecruitInfo firstRecruit, RecruitInfo secondRecruit)
        {
            var rcpDirectory = CreateDirectory(parameters, firstRecruit);
            var fileName = GetFileName(firstRecruit, secondRecruit);

            var filePath = Path.Combine(rcpDirectory,
                $"{fileName}{Path.GetExtension(_exportTemplateFilePath)}");

            return _recruitExcelExporterFactory.Create(_exportTemplateFilePath, filePath);
        }

        private string CreateDirectory(ExportRecruitCommandParameters parameters, 
            RecruitInfo firstRecruit)
        {
            var rcpDirectory = Path.Combine(_exportDirectoryPath,
                firstRecruit.ServiceInfo.ConscriptionDate.ToString("D"),
                firstRecruit.ServiceInfo.RegionalCollectionPoint);

            if (!Directory.Exists(rcpDirectory))
            {
                Directory.CreateDirectory(rcpDirectory);
            }

            return rcpDirectory;
        }

        private string GetFileName(RecruitInfo firstRecruit, RecruitInfo secondRecruit)
        {
            string fullName1 = firstRecruit.Envelope.PassportInfo.PersonInfo.FullName.Value;

            string fullName2 = null;
            if (secondRecruit != null)
            {
                fullName2 = secondRecruit.Envelope.PassportInfo.PersonInfo.FullName.Value;
            }

            return fullName2 != null 
                ? $"[{fullName1}] [{fullName2}]"
                : $"[{fullName1}]";
        }
    }
}
