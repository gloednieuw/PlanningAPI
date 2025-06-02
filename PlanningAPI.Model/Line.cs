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
        public int LineId { get; private set; }
        public int OperatorNo { get; private set; }
        public string LinePlanningNumber { get; private set; }

        private bool IsValid(int lineId, int operatorNo, string linePlanningNumber, List<string> validationErrors)
        {
            return true;
        }

        public IReadOnlyCollection<Trip> Trips { get; set; }
    }
}
