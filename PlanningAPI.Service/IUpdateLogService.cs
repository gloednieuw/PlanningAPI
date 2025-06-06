using PlanningAPI.Service.Dto;

namespace PlanningAPI.Service
{
	public interface IUpdateLogService
    {
        Task<bool> AddUpdateLogEntryForTrip(UpdateLogAddDto updateLogAddDto);
    }
}
