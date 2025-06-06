using PlanningAPI.Model;
using PlanningAPI.Service.Dto;
using System;

namespace PlanningAPI.Service
{
    public interface IDomainServices
    {
        Task<bool> TripExists(int tripId);
    }
}
