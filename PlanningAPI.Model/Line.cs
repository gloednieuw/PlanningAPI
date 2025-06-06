namespace PlanningAPI.Model
{
    public class Line : IEntity
    {
        public Line(int lineId, int operatorNo, string linePlanningNumber)
        {
            List<string> validationErrors = new();

            if (!IsValid(lineId, operatorNo, linePlanningNumber, validationErrors))
            {
                throw new ArgumentException(validationErrors.ToValidationString());
            }

            LineId = lineId;
            OperatorNo = operatorNo;
            LinePlanningNumber = linePlanningNumber;
        }

        public Line(int operatorNo, string linePlanningNumber)
        {
            List<string> validationErrors = new();

            if (!IsValid(operatorNo, linePlanningNumber, validationErrors))
            {
                throw new ArgumentException(validationErrors.ToValidationString());
            }

            OperatorNo = operatorNo;
            LinePlanningNumber = linePlanningNumber;
        }

        public int LineId { get; private set; }
        public int OperatorNo { get; private set; }
        public string LinePlanningNumber { get; private set; }

        private bool IsValid(int operatorNo, string linePlanningNumber, List<string> validationErrors)
        {
            if (linePlanningNumber.Length < 3)
            {
                validationErrors.Add("Line planning number must be at least 3 characters long.");
            }

            if (!linePlanningNumber.All(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c)))
            {
                validationErrors.Add("Line planning number must be alphanumeric.");
            }

            //etc. etc.

            return !validationErrors.Any();
        }

        private bool IsValid(int lineId, int operatorNo, string linePlanningNumber, List<string> validationErrors)
        {
            if (!IsValid(operatorNo, linePlanningNumber, validationErrors))
            {
                return false;
            }

            return true;
        }

        private readonly List<Trip> _trips = new();
        public IReadOnlyCollection<Trip> Trips => _trips.AsReadOnly();

        public void AddTrip(Trip trip)
        {
            if (_trips.Any(l => l.DepartureTime == trip.DepartureTime && l.ArrivalTime == trip.ArrivalTime))
            {
                throw new InvalidOperationException("Trip with the same departure and arrival times already exists.");
            }

            _trips.Add(trip);
        }
    }
}
