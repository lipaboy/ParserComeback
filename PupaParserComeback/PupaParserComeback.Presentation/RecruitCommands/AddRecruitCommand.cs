using PupaParserComeback.Domain.DomainModels;
using PupaParserComeback.Domain.Interfaces;
using PupaParserComeback.Presentation.Abstract;
using PupaParserComeback.Presentation.Enums;
using PupaParserComeback.Presentation.Interfaces;
using PupaParserComeback.Presentation.RecruitCommands.Parameters;
using System;
using System.Threading.Tasks;

namespace PupaParserComeback.Presentation.RecruitCommands
{
    public class AddRecruitCommand : IParameterizedCommandAsync<AddRecruitCommandParameters>
    {
        public const string CommandName = "Добавление призывника";
        public const string CommandSuccess = "Призывник добавлен";

        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IRecruitInfoRepository _recruitInfoRepository;
        private readonly IEventService _eventService;

        public AddRecruitCommand(IUnitOfWorkFactory unitOfWorkFactory,
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

        public async Task ExecuteAsync(AddRecruitCommandParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            using (var unitOfWork = _unitOfWorkFactory.Create())
            {
                try
                {
                    var message = $"{CommandSuccess} - {parameters.RecruitInfo.Envelope.PassportInfo.PersonInfo.FullName.Value}";

                    await _recruitInfoRepository.AddAsync(parameters.RecruitInfo);
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
