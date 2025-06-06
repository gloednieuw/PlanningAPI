using PlanningAPI.Model;
using PlanningAPI.Service.Dto;
using System;

namespace PlanningAPI.Service
{
    public class UpdateLogService : IUpdateLogService
    {
        private readonly IRepository<UpdateLog> _repository;
        private readonly IDomainServices _domainServices;

        public UpdateLogService(IRepository<UpdateLog> repository, IDomainServices domainServices)
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
