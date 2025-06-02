namespace PlanningAPI.Model
{
    public class UpdateLog : IEntity
    {
        public UpdateLog(int updateLogId, int tripNo, DateTime updateTimestamp, UpdateLogStatus status)
        {
            List<string> validationErrors = new();

            if (!IsValid(updateLogId, tripNo, updateTimestamp, status, validationErrors))
            {
                throw new ArgumentException(validationErrors.ToString());
            }

            UpdateLogId = updateLogId;
            TripNo = tripNo;
            UpdateTimestamp = updateTimestamp;
            Status = status;
        }
        public int UpdateLogId { get; private set; }
        public int TripNo { get; private set; }
        public DateTime UpdateTimestamp { get; private set; }
        public UpdateLogStatus Status { get; private set; }

        private bool IsValid(int updateLogId, int tripNo, DateTime updateTimestamp, UpdateLogStatus status, List<string> validationErrors)
        {
            return true;
        }
    }
}
