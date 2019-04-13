using PupaParserComeback.Domain.Interfaces;
using PupaParserComeback.Presentation.Abstract;
using PupaParserComeback.Presentation.Enums;
using PupaParserComeback.Presentation.Interfaces;
using PupaParserComeback.Presentation.Models;
using PupaParserComeback.Presentation.RecruitCommands.Parameters;
using System;
using System.Threading.Tasks;

namespace PupaParserComeback.Presentation.RecruitCommands
{
    public class RemoveRecruitCommand : IParameterizedCommandAsync<RemoveRecruitCommandParameters>
    {
        public const string CommandName = "Удаление призывника";
        public const string CommandSuccess = "Призывник удален";

        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IRecruitInfoRepository _recruitInfoRepository;
        private readonly IEventService _eventService;

        public RemoveRecruitCommand(IUnitOfWorkFactory unitOfWorkFactory,
            IRecruitInfoRepository recruitInfoRepository,
            IEventService eventService)
        {
            if (unitOfWorkFactory == null)
            {
                throw new ArgumentNullException(nameof(unitOfWorkFactory));
            }

            if (recruitInfoRepository == null)
            {
                throw new ArgumentNullException(nameof(recruitInfoRepository));
            }

            if (eventService == null)
            {
                throw new ArgumentNullException(nameof(eventService));
            }

            _unitOfWorkFactory = unitOfWorkFactory;
            _recruitInfoRepository = recruitInfoRepository;
            _eventService = eventService;
        }

        public async Task ExecuteAsync(RemoveRecruitCommandParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            var fullName = $"{parameters.RecruitShortUIModel.Surname} {parameters.RecruitShortUIModel.Name} {parameters.RecruitShortUIModel.Patronymic}";
            var message = $"{CommandSuccess} - {fullName}";

            if (!parameters.RecruitShortUIModel.SqliteId.HasValue)
            {
                parameters.StateChanged.OnStateChanged(message, StateResult.Success);
                return;
            }

            using (var unitOfWork = _unitOfWorkFactory.Create())
            {
                try
                {
                    await _recruitInfoRepository.RemoveAsync(parameters.RecruitShortUIModel.SqliteId.Value);
                    _eventService.Fire(message);

                    unitOfWork.Commit();
                    parameters.StateChanged.OnStateChanged(message, StateResult.Success);
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();

                    throw ex;
                }
            }
        }
    }
}
