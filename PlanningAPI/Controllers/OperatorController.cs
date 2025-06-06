using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using PlanningAPI.Model;
using PlanningAPI.Service;
using PlanningAPI.Service.Dto;

namespace PlanningAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OperatorController : ControllerBase
    {
        private readonly IOperatorService _service;

        public OperatorController(IOperatorService service)
        {
            _service = service;
        }

        /// <summary>
        /// Gets a list of all operators.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OperatorListDto>>> Get()
        {
            return new ActionResult<IEnumerable<OperatorListDto>>(await _service.GetAllOperators());
        }

        /// <summary>
        /// Gets detailed information about a specific operator, including its lines.
        /// </summary>
        /// <param name="operatorId">The ID of the operator.</param>
        [HttpGet("Details")]
        public async Task<ActionResult<OperatorViewDto>> Details([FromQuery] int operatorId)
        {
            return new ActionResult<OperatorViewDto>(await _service.GetOperatorDetails(operatorId));
        }

        /// <summary>
        /// Adds a new line to an existing operator.
        /// </summary>
        /// <param name="lineAddDto">The line data to add.</param>
        [HttpPost("Add/Line")]
        public async Task<IActionResult> AddLine([FromBody] LineAddDto lineAddDto)
        {
            var result = await _service.AddLine(lineAddDto);
            if (result)
            {
                return Ok("Line added successfully.");
            }

            return BadRequest("Failed to add line. Operator not found or invalid data provided.");
        }

        /// <summary>
        /// Adds a new trip to a specific line of an operator.
        /// </summary>
        /// <param name="tripAddDto">The trip data to add.</param>
        [HttpPost("Add/Lines/Trip")]
        public async Task<IActionResult> AddTrip([FromBody] TripAddDto tripAddDto)
        {
            var result = await _service.AddTripToLine(tripAddDto);
            if (result)
            {
                return Ok("Trip added successfully.");
            }

            return BadRequest("Failed to add trip. Operator or line not found, or invalid data provided.");
        }

        /// <summary>
        /// Gets all upcoming trips for a specific line of an operator.
        /// </summary>
        /// <param name="operatorId">The ID of the operator.</param>
        /// <param name="lineId">The ID of the line.</param>
        [HttpGet("{operatorId}/lines/{lineId}/trips/upcoming")]
        public async Task<ActionResult<IEnumerable<TripListDto>>> GetUpcomingTrips(int operatorId, int lineId)
        {
            var trips = await _service.GetUpcomingTrips(operatorId, lineId);
            return new ActionResult<IEnumerable<TripListDto>>(trips);
        }

        // TODO endpoints for e.g. updating a line, deleting a trip, etc.
    }
}
