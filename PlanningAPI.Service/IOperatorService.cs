using PlanningAPI.Service.Dto;

namespace PlanningAPI.Service
{
	public interface IOperatorService
    {
        Task<IEnumerable<OperatorListDto>> GetAllOperators();
        Task<OperatorViewDto> GetOperatorDetails(int operatorId);
        Task<bool> AddLine(LineAddDto lineAddDto);
        Task<bool> AddTripToLine(TripAddDto tripAddDto);
        Task<IEnumerable<TripListDto>> GetUpcomingTrips(int operatorId, int lineId);
    }
}
