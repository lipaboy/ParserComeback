using PupaParserComeback.Domain.DomainModels;
using PupaParserComeback.Domain.Interfaces;
using PupaParserComeback.Presentation.Abstract;
using PupaParserComeback.Presentation.Constants;
using PupaParserComeback.Presentation.Enums;
using PupaParserComeback.Presentation.Interfaces;
using PupaParserComeback.Presentation.RecruitCommands.Parameters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PupaParserComeback.Presentation.RecruitCommands
{
    public class TransmitRecruitCommand : IParameterizedCommandAsync<TransmitRecruitCommandParameters>
    {
        public const string CommandName = "Сохранить в базу данных формирования";
        public const string CommandSuccess = "Сохранение в базу данных формирования выполнено";

        private readonly IUnitOfWorkFactory _firebirdUnitOfWorkFactory;
        private readonly IUnitOfWorkFactory _sqliteUnitOfWorkFactory;

        private readonly ITransmitService _transmitService;
        private readonly IRecruitInfoRepository _recruitInfoRepository;
        
        private readonly IEventService _eventService;

        public TransmitRecruitCommand(IUnitOfWorkFactory firebirdUnitOfWorkFactory,
            IUnitOfWorkFactory sqliteUnitOfWorkFactory,
            ITransmitService transmitService, 
            IRecruitInfoRepository recruitInfoRepository,
            IEventService eventService)
        {
            if (firebirdUnitOfWorkFactory == null)
            {
                throw new ArgumentNullException(nameof(firebirdUnitOfWorkFactory));
            }

            if (sqliteUnitOfWorkFactory == null)
            {
                throw new ArgumentNullException(nameof(sqliteUnitOfWorkFactory));
            }

            if (transmitService == null)
            {
                throw new ArgumentNullException(nameof(transmitService));
            }

            if (recruitInfoRepository == null)
            {
                throw new ArgumentNullException(nameof(recruitInfoRepository));
            }

            if (eventService == null)
            {
                throw new ArgumentNullException(nameof(eventService));
            }

            _firebirdUnitOfWorkFactory = firebirdUnitOfWorkFactory;
            _sqliteUnitOfWorkFactory = sqliteUnitOfWorkFactory;

            _transmitService = transmitService;
            _recruitInfoRepository = recruitInfoRepository;
            
            _eventService = eventService;
        }

        public async Task ExecuteAsync(TransmitRecruitCommandParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            var recruitInfoes = _recruitInfoRepository.Get(parameters.RecruitIds)
                .ToList()
                .OrderBy(r => r.Envelope.PassportInfo.PersonInfo.FullName.Surname)
                .ToList();

            using (var firebirdUnitOfWork = _firebirdUnitOfWorkFactory.Create())
            using (var sqliteUnitOfWork = _sqliteUnitOfWorkFactory.Create())
            {
                try
                {
                    var regionalCollectionPoints = recruitInfoes.Select(r => r.ServiceInfo.RegionalCollectionPoint).Distinct().ToList();
                    var message = $"{CommandSuccess}. Военкоматы - {string.Join(SeparatorConstants.CommaSeparator, regionalCollectionPoints)}";

                    await _transmitService.MoveAsync(recruitInfoes);

                    foreach (var recruit in recruitInfoes)
                    {
                        ChangePhotoName(recruit);

                        _recruitInfoRepository.Change(recruit);
                    }

                    _eventService.Fire(message);

                    await firebirdUnitOfWork.CommitAsync();
                    await sqliteUnitOfWork.CommitAsync();

                    parameters.StateChanged.OnStateChanged(message, StateResult.Success);
                }
                catch (Exception ex)
                {
                    firebirdUnitOfWork.Rollback();
                    sqliteUnitOfWork.Rollback();

                    throw ex;
                }
            }
        }

        private void ChangePhotoName(RecruitInfo recruit)
        {
            if (recruit.ServiceInfo.FirebirdId.HasValue)
            {
                var extension = Path.GetExtension(recruit.Envelope.PassportInfo.PhotoName);
                var newPhotoName = $"{recruit.ServiceInfo.FirebirdId.Value}{extension}";

                recruit.Envelope.PassportInfo.ChangePhotoName(newPhotoName);
            }
        }
    }
}
