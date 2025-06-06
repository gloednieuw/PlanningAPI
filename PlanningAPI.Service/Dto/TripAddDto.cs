using PlanningAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanningAPI.Service.Dto
{
    public class TripAddDto
    {
        public int OperatorId { get; set; }
        public int LineId { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }

        public static Trip ToEntity(TripAddDto dto)
        {
            return new Trip(dto.LineId, dto.DepartureTime, dto.ArrivalTime);
        }
    }
}
