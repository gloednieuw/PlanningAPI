using PlanningAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanningAPI.Service.Dto
{
    public class LineListDto
    {
        public int LineId { get; }
        public string LinePlanningNumber { get; }

        public LineListDto() { }

        private LineListDto(int lineId, string linePlanningNumber)
        {
            LineId = lineId;
            LinePlanningNumber = linePlanningNumber;
        }

        public static LineListDto FromEntity(Line lineEntity)
        {
            return new LineListDto(lineEntity.LineId, lineEntity.LinePlanningNumber);
        }
    }
}
