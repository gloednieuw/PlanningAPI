using PlanningAPI.Model;
using PlanningAPI.Service.Dto;
using System;

namespace PlanningAPI.Service
{
    public class UpdateLogService : IEntityService<UpdateLog>
    {
        private readonly IRepository<UpdateLog> _repository;
        private readonly DomainServices _domainServices;

        public UpdateLogService(IRepository<UpdateLog> repository, DomainServices domainServices)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _domainServices = domainServices ?? throw new ArgumentNullException(nameof(_domainServices));
        }

        public async Task<bool> AddUpdateLogEntryForTrip(UpdateLogAddDto updateLogAddDto)
        {
            bool tripExists = await _domainServices.TripExists(updateLogAddDto.TripNo);

            if (!tripExists)
            {
                throw new ArgumentException($"Trip with ID {updateLogAddDto.TripNo} not found");
            }

            var updateLog = UpdateLogAddDto.ToEntity(updateLogAddDto);

            var success = await _repository.AddAsync(updateLog);

            return success;
        }
    }
}
