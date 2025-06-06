namespace PlanningAPI.Model
{
    public class UpdateLog : IEntity
    {
        public UpdateLog(int updateLogId, int tripNo, DateTime updateTimestamp, UpdateLogStatus status)
        {
            List<string> validationErrors = new();

            if (!IsValid(updateLogId, tripNo, updateTimestamp, status, validationErrors))
            {
                throw new ArgumentException(validationErrors.ToValidationString());
            }

            UpdateLogId = updateLogId;
            TripNo = tripNo;
            UpdateTimestamp = updateTimestamp;
            Status = status;
        }

        public UpdateLog(int tripNo, DateTime updateTimestamp, UpdateLogStatus status)
        {
            List<string> validationErrors = new();

            if (!IsValid(tripNo, updateTimestamp, status, validationErrors))
            {
                throw new ArgumentException(validationErrors.ToValidationString());
            }

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
            if (!IsValid(tripNo, updateTimestamp, status, validationErrors))
            {
                return false;
            }

            if (updateLogId <= 0)
            {
                validationErrors.Add("Update log is missing data");
                return false;
            }

            return true;
        }

        private bool IsValid(int tripNo, DateTime updateTimestamp, UpdateLogStatus status, List<string> validationErrors)
        {
            if (tripNo <= 0 || updateTimestamp == DateTime.MinValue || status == 0)
            {
                validationErrors.Add("Update log is missing data");
                return false;
            }

            if (updateTimestamp > DateTime.Now)
            {
                validationErrors.Add("Update moment cannot be in the future");
                return false;
            }
            return true;
        }
    }
}
