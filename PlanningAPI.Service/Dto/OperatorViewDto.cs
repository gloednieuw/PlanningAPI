using PlanningAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanningAPI.Service.Dto
{
    public class OperatorViewDto
    {
        public int OperatorId { get; }
        public string Name { get; }
        public string ApiEndpoint { get; }
        public IEnumerable<LineListDto> Lines { get; }

        public OperatorViewDto() { }

        private OperatorViewDto(int operatorId, string name, string apiEndpoint, IEnumerable<Line> lines)
        {
            OperatorId = operatorId;
            Name = name;
            ApiEndpoint = apiEndpoint;
            Lines = lines.Select(x => LineListDto.FromEntity(x)).ToList();
        }

        public static OperatorViewDto FromEntity(Operator operatorEntity)
        {
            return new OperatorViewDto(operatorEntity.OperatorId, operatorEntity.Name, operatorEntity.ApiEndpoint, operatorEntity.Lines);
        }
    }
}
