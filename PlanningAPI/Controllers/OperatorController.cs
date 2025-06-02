using Microsoft.AspNetCore.Mvc;
using PlanningAPI.Model;
using PlanningAPI.Service;
using PlanningAPI.Service.Dto;

namespace PlanningAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OperatorController : ControllerBase
    {
        private readonly OperatorService _service;
        private readonly DomainServices _domainServices;

        public OperatorController(OperatorService service, DomainServices domainServices)
        {
            _service = service;
            _domainServices = domainServices;
        }

        [HttpGet]
        public async Task<IEnumerable<OperatorListDto>> Get()
        {
            return await _service.GetAllOperators();
        }

        [HttpGet("Details")]
        public async Task<OperatorViewDto> Details([FromQuery] int operatorId)
        {
            return await _service.GetOperatorDetails(operatorId);
        }
    }
}
