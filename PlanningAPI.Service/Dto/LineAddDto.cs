using PlanningAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanningAPI.Service.Dto
{
    public class LineAddDto
    {
        public LineAddDto() { }

        public LineAddDto(int operatorNo, string linePlanningNumber) 
        {
            OperatorNo = operatorNo;
            LinePlanningNumber = linePlanningNumber;
        }

        public int OperatorNo { get; set; }
        public string LinePlanningNumber { get; set; }

        public static Line ToEntity(LineAddDto lineAddDto)
        {
            return new Line(lineAddDto.OperatorNo, lineAddDto.LinePlanningNumber);
        }
    }
}
