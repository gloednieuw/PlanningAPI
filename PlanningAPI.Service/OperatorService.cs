using PlanningAPI.Model;
using PlanningAPI.Service.Dto;
using System;

namespace PlanningAPI.Service
{
    public class OperatorService : IEntityService<Operator>
    {
        private readonly IRepository<Operator> _repository;

        public OperatorService(IRepository<Operator> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<IEnumerable<OperatorListDto>> GetAllOperators()
        {
            var operators = await _repository.ListAsync();

            return operators.Select(x => OperatorListDto.FromEntity(x)).ToList();
        }

        public async Task<OperatorViewDto> GetOperatorDetails(int operatorId)
        {
            var operatorEntity = await _repository.GetByIdAsync(operatorId);

            return OperatorViewDto.FromEntity(operatorEntity);
        }

        public async Task<bool> AddLine(LineAddDto lineAddDto)
        {
            var operatorEntity = await _repository.GetByIdAsync(lineAddDto.OperatorNo);
            if (operatorEntity == null)
            {
                throw new ArgumentException($"Operator with ID {lineAddDto.OperatorNo} not found");
            }

            var line = LineAddDto.ToEntity(lineAddDto);

            operatorEntity.AddLine(line);

            return await _repository.UpdateAsync(operatorEntity);
        }

        public async Task<bool> AddTripToLine(TripAddDto tripAddDto)
        {
            var operatorEntity = await _repository.GetByIdAsync(tripAddDto.OperatorId);
            if (operatorEntity == null)
            {
                throw new ArgumentException($"Operator with ID {tripAddDto.OperatorId} not found");
            }
            
            var line = operatorEntity.Lines.FirstOrDefault(l => l.LineId == tripAddDto.LineId);

            if (line == null)
            {
                throw new ArgumentException($"Line with ID {tripAddDto.LineId} not found for operator {tripAddDto.OperatorId}.");
            }

            var trip = TripAddDto.ToEntity(tripAddDto);

            line.AddTrip(trip);

            return await _repository.UpdateAsync(operatorEntity);
        }

        public async Task<IEnumerable<TripListDto>> GetUpcomingTrips(int operatorId, int lineId)
        {
            var operatorEntity = await _repository.GetByIdAsync(operatorId);
            if (operatorEntity == null)
            {
                throw new ArgumentException($"Operator with ID {operatorId} not found");
            }

            var line = operatorEntity.Lines.FirstOrDefault(l => l.LineId == lineId);
            if (line == null)
            {
                throw new ArgumentException($"Line with ID {lineId} not found for operator {operatorId}.");
            }

            var upcomingTrips = line.Trips
                .Where(t => t.DepartureTime > DateTime.Now)
                .OrderBy(t => t.DepartureTime)
                .Select(t => TripListDto.FromEntity(t))
                .ToList();

            return upcomingTrips;
        }
    }
}
