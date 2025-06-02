using PlanningAPI.Model;
using PlanningAPI.Service.Dto;
using System;

namespace PlanningAPI.Service
{
    public class OperatorService : IEntityService<Operator>
    {
        private readonly IRepository<Operator> _repository;

        public OperatorService(IRepository<Operator> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<IEnumerable<OperatorListDto>> GetAllOperators()
        {
            var operators = await _repository.ListAsync();

            return operators.Select(x => OperatorListDto.FromEntity(x)).ToList();
        }

        public async Task<OperatorViewDto> GetOperatorDetails(int operatorId)
        {
            var operatorEntity = await _repository.GetByIdAsync(operatorId);

            return OperatorViewDto.FromEntity(operatorEntity);
        }
    }
}
