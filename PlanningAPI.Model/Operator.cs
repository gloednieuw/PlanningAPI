namespace PlanningAPI.Model
{
    public class Operator : IEntity
    {
        public Operator(int operatorId, string name, string apiEndpoint) 
        {
            List<string> validationErrors = new();

            if (!IsValid(operatorId, name, apiEndpoint, validationErrors))
            {
                throw new ArgumentException(validationErrors.ToValidationString());
            }

            OperatorId = operatorId;
            Name = name;
            ApiEndpoint = apiEndpoint;
        }
        public int OperatorId { get; private set; }
        public string Name { get; private set; }
        public string ApiEndpoint { get; private set; }

        private bool IsValid(int operatorId, string name, string apiEndpoint, List<string> validationErrors)
        {
            return true;
        }

        private readonly List<Line> _lines = new();
        public IReadOnlyCollection<Line> Lines => _lines.AsReadOnly();

        public void AddLine(Line line)
        {
            if (_lines.Any(l => l.LinePlanningNumber == line.LinePlanningNumber))
            {
                throw new InvalidOperationException("Line with the same planning number already exists.");
            }

            _lines.Add(line);
        }
    }
}
