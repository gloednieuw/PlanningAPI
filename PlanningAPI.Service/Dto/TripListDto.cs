using PlanningAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanningAPI.Service.Dto
{
    public class TripListDto
    {
        public int TripId { get; set; }
        public int LineNo { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }

        public TripListDto() { }

        public static TripListDto FromEntity(Trip trip)
        {
            return new TripListDto
            {
                TripId = trip.TripId,
                LineNo = trip.LineNo,
                DepartureTime = trip.DepartureTime,
                ArrivalTime = trip.ArrivalTime
            };
        }
    }
}
