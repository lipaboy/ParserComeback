using PupaParserComeback.Presentation.Abstract;
using PupaParserComeback.Presentation.Enums;
using PupaParserComeback.Presentation.Interfaces;
using PupaParserComeback.Presentation.RecruitCommands.Parameters;
using System;
using System.Threading.Tasks;

namespace PupaParserComeback.Presentation.RecruitCommands
{
    public class ImportRecruitsCommand : IParameterizedCommandAsync<ImportRecruitCommandParameters>
    {
        public const string CommandName = "Импорт призывников из файлов";
        public const string CommandSuccess = "Импорт выполнен";

        private readonly IRecruitImporter _recruitImporter;
        private readonly IImportedRecruitRepository _importedRecruitRepository;

        public ImportRecruitsCommand(IRecruitImporter recruitImporter, 
            IImportedRecruitRepository importedRecruitRepository)
        {
            if (recruitImporter == null)
            {
                throw new ArgumentNullException(nameof(recruitImporter));
            }

            if (importedRecruitRepository == null)
            {
                throw new ArgumentNullException(nameof(importedRecruitRepository));
            }

            _recruitImporter = recruitImporter;
            _importedRecruitRepository = importedRecruitRepository;
        }

        public async Task ExecuteAsync(ImportRecruitCommandParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            await _recruitImporter.CopyPersonalPhotosAsync(parameters.ConscriptionDate, 
                parameters.SelectedRegionalCollectionPoint);

            var recruitShortUIModels = await _recruitImporter.ImportRecruitShortUIModelsAsync(parameters.ConscriptionDate, 
                parameters.SelectedRegionalCollectionPoint);

            _importedRecruitRepository.AddRange(recruitShortUIModels);

            parameters.StateChanged.OnStateChanged(CommandSuccess, StateResult.Success);
        }
    }
}
