using PlanningAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanningAPI.Service.Dto
{
    public class UpdateLogAddDto
    {
        public UpdateLogAddDto() { }

        public UpdateLogAddDto(int tripNo, DateTime updateTimestamp, UpdateLogStatus status) 
        {
            TripNo = tripNo;
            UpdateTimestamp = updateTimestamp;
            Status = status;
        }

        public int TripNo { get; }
        public DateTime UpdateTimestamp { get; }
        public UpdateLogStatus Status { get; }

        public static UpdateLog ToEntity(UpdateLogAddDto updateLogAddDto)
        {
            return new UpdateLog(updateLogAddDto.TripNo, updateLogAddDto.UpdateTimestamp, updateLogAddDto.Status);
        }
    }
}
