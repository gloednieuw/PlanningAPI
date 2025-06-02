using PlanningAPI.Model;
using PlanningAPI.Service.Dto;
using System;

namespace PlanningAPI.Service
{
    public class DomainServices
    {
        private readonly IRepository<Operator> _operatorRepository;
        private readonly IRepository<Trip> _tripRepository;
        private readonly IRepository<UpdateLog> _updateLogRepository;

        public DomainServices(IRepository<Operator> operatorRepository, IRepository<Trip> tripRepository, IRepository<UpdateLog> updateLogRepository)
        {
            _operatorRepository = operatorRepository ?? throw new ArgumentNullException(nameof(operatorRepository));
            _tripRepository = tripRepository ?? throw new ArgumentNullException(nameof(tripRepository));
            _updateLogRepository = updateLogRepository ?? throw new ArgumentNullException(nameof(updateLogRepository));
        }

        public async Task<bool> TripExists(int tripId)
        {
            return await _tripRepository.Exists(tripId);
        }
    }
}
