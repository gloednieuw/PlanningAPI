using System.Net;
using System.Xml.Linq;

namespace PlanningAPI.Model
{
    public class Trip : IEntity
    {
        public Trip(int tripId, int lineNo, DateTime departureTime, DateTime arrivalTime)
        {
            List<string> validationErrors = new();

            if (!IsValid(tripId, lineNo, departureTime, arrivalTime, validationErrors))
            {
                throw new ArgumentException(validationErrors.ToValidationString());
            }

            TripId = tripId;
            LineNo = lineNo;
            DepartureTime = departureTime;
            ArrivalTime = arrivalTime;
        }

        public Trip(int lineNo, DateTime departureTime, DateTime arrivalTime)
        {
            List<string> validationErrors = new();

            if (!IsValid(lineNo, departureTime, arrivalTime, validationErrors))
            {
                throw new ArgumentException(validationErrors.ToValidationString());
            }

            LineNo = lineNo;
            DepartureTime = departureTime;
            ArrivalTime = arrivalTime;
        }

        public int TripId { get; private set; }
        public int LineNo { get; private set; }
        public DateTime DepartureTime { get; private set; }
        public DateTime ArrivalTime { get; private set; }

        private bool IsValid(int lineNo, DateTime departureTime, DateTime arrivalTime, List<string> validationErrors)
        {
            return true;
        }

        private bool IsValid(int tripId, int lineNo, DateTime departureTime, DateTime arrivalTime, List<string> validationErrors)
        {
            if (!IsValid(lineNo, departureTime, arrivalTime, validationErrors))
            {
                return false;
            }

            return true;
        }

        public IReadOnlyCollection<UpdateLog> UpdateLogs { get; set; }
    }
}
