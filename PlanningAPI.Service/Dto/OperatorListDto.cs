using PlanningAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanningAPI.Service.Dto
{
    public class OperatorListDto
    {
        public int OperatorId { get; }
        public string Name { get; }
        public string ApiEndpoint { get; }

        private OperatorListDto(int operatorId, string name, string apiEndpoint)
        {
            OperatorId = operatorId;
            Name = name;
            ApiEndpoint = apiEndpoint;
        }

        public static OperatorListDto FromEntity(Operator operatorEntity)
        {
            return new OperatorListDto(operatorEntity.OperatorId, operatorEntity.Name, operatorEntity.ApiEndpoint);
        }
    }
}
