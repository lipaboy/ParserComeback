using PupaParserComeback.Presentation.Abstract;
using PupaParserComeback.Presentation.Enums;
using PupaParserComeback.Presentation.Interfaces;
using PupaParserComeback.Presentation.Models;
using PupaParserComeback.Presentation.RecruitCommands.Parameters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PupaParserComeback.Presentation.RecruitCommands
{
    public class ExportTableRecruitCommand : IParameterizedCommandAsync<ExportTableRecruitCommandParameters>
    {
        private const string AllRegionalCollectionPoints = "Все военкоматы";

        public const string CommandName = "Экспорт призывников в таблицу Excel";
        public const string CommandSuccess = "Экспорт призывников в таблицу Excel выполнен";

        private readonly IRecruitExcelExporterFactory _recruitExcelExporterFactory;

        private readonly string _exportTableTemplateFilePath;
        private readonly string _exportDirectoryPath;
        
        public ExportTableRecruitCommand(IRecruitExcelExporterFactory recruitExcelExporterFactory,
            string exportTableTemplateFilePath,
            string exportDirectoryPath)
        {
            if (recruitExcelExporterFactory == null)
            {
                throw new ArgumentNullException(nameof(recruitExcelExporterFactory));
            }

            if (string.IsNullOrWhiteSpace(exportTableTemplateFilePath))
            {
                throw new ArgumentNullException(nameof(exportTableTemplateFilePath));
            }

            if (string.IsNullOrWhiteSpace(exportDirectoryPath))
            {
                throw new ArgumentNullException(nameof(exportDirectoryPath));
            }


            _recruitExcelExporterFactory = recruitExcelExporterFactory;

            _exportTableTemplateFilePath = exportTableTemplateFilePath;
            _exportDirectoryPath = exportDirectoryPath;
        }

        public async Task ExecuteAsync(ExportTableRecruitCommandParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            await Task.Run(() =>
            {
                var conscriptionDateExportDirectory = CreateDirectory(parameters);

                var fileName = !string.IsNullOrWhiteSpace(parameters.RegionalCollectionPoint)
                    ? parameters.RegionalCollectionPoint
                    : AllRegionalCollectionPoints;

                var filePath = Path.Combine(conscriptionDateExportDirectory,
                    $"{fileName}{Path.GetExtension(_exportTableTemplateFilePath)}");

                var recruitExporter = _recruitExcelExporterFactory.Create(_exportTableTemplateFilePath, 
                    filePath);

                recruitExporter.ExportRecruitShortUIModelsToExcelTable(parameters.RecruitShortUIModels,
                    parameters.RegionalCollectionPoint,
                    parameters.ConscriptionDate);

                parameters.StateChanged.OnStateChanged(CommandSuccess, StateResult.Success);
            });
        }


        private string CreateDirectory(ExportTableRecruitCommandParameters parameters)
        {
            var conscriptionDateExportDirectory = Path.Combine(_exportDirectoryPath,
                parameters.ConscriptionDate);

            if (!Directory.Exists(conscriptionDateExportDirectory))
            {
                Directory.CreateDirectory(conscriptionDateExportDirectory);
            }

            return conscriptionDateExportDirectory;
        }
    }
}
