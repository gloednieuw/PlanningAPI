using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlanningAPI;
using PlanningAPI.Service;
using PlanningAPI.Service.Dto;
using PlanningAPI.Model;
using PlanningAPI.Controllers;

namespace PlanningAPI.Tests
{
    public class OperatorControllerTests
    {
        private readonly Mock<IOperatorService> _mockService;
        private readonly OperatorController _controller;

        public OperatorControllerTests()
        {
            _mockService = new Mock<IOperatorService>();
            _controller = new OperatorController(_mockService.Object);
        }

        [Fact]
        public async Task Get_ReturnsListOfOperators()
        {
            // Arrange
            var operators = new List<OperatorListDto>
            {
                OperatorListDto.FromEntity(new Operator(1, "Test1", "api1")),
                OperatorListDto.FromEntity(new Operator(2, "Test2", "api2"))
            };
            _mockService.Setup(s => s.GetAllOperators()).ReturnsAsync(operators);

            // Act
            var result = await _controller.Get();

            // Assert
            var okResult = Assert.IsType<ActionResult<IEnumerable<OperatorListDto>>>(result);
            Assert.Equal(2, ((IEnumerable<OperatorListDto>)okResult.Value).AsList().Count);
        }

        [Fact]
        public async Task Details_ReturnsOperatorViewDto_WhenFound()
        {
            // Arrange
            var dto = OperatorViewDto.FromEntity(new PlanningAPI.Model.Operator(1, "Test", "api"));
            _mockService.Setup(s => s.GetOperatorDetails(1)).ReturnsAsync(dto);

            // Act
            var result = await _controller.Details(1);

            // Assert
            var okResult = Assert.IsType<ActionResult<OperatorViewDto>>(result);
            Assert.Equal(1, okResult.Value.OperatorId);
        }

        [Fact]
        public async Task AddLine_ReturnsOk_WhenSuccess()
        {
            // Arrange
            var dto = new LineAddDto(1, "ABC123");
            _mockService.Setup(s => s.AddLine(dto)).ReturnsAsync(true);

            // Act
            var result = await _controller.AddLine(dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Line added successfully.", okResult.Value);
        }

        [Fact]
        public async Task AddLine_ReturnsBadRequest_WhenFailed()
        {
            // Arrange
            var dto = new LineAddDto(1, "ABC123");
            _mockService.Setup(s => s.AddLine(dto)).ReturnsAsync(false);

            // Act
            var result = await _controller.AddLine(dto);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Failed to add line. Operator not found or invalid data provided.", badRequest.Value);
        }

        [Fact]
        public async Task AddTrip_ReturnsOk_WhenSuccess()
        {
            // Arrange
            var dto = new TripAddDto { OperatorId = 1, LineId = 1 };
            _mockService.Setup(s => s.AddTripToLine(dto)).ReturnsAsync(true);

            // Act
            var result = await _controller.AddTrip(dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Trip added successfully.", okResult.Value);
        }

        [Fact]
        public async Task AddTrip_ReturnsBadRequest_WhenFailed()
        {
            // Arrange
            var dto = new TripAddDto { OperatorId = 1, LineId = 1 };
            _mockService.Setup(s => s.AddTripToLine(dto)).ReturnsAsync(false);

            // Act
            var result = await _controller.AddTrip(dto);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Failed to add trip. Operator or line not found, or invalid data provided.", badRequest.Value);
        }

        [Fact]
        public async Task GetUpcomingTrips_ReturnsTrips()
        {
            // Arrange
            var trips = new List<TripListDto>
            {
                new TripListDto { TripId = 1, LineNo = 1 },
                new TripListDto { TripId = 2, LineNo = 1 }
            };
            _mockService.Setup(s => s.GetUpcomingTrips(1, 1)).ReturnsAsync(trips);

            // Act
            var result = await _controller.GetUpcomingTrips(1, 1);

            // Assert
            var okResult = Assert.IsType<ActionResult<IEnumerable<TripListDto>>>(result);
            Assert.Equal(2, ((IEnumerable<TripListDto>)okResult.Value).AsList().Count);
        }
    }

    // Helper extension for IEnumerable<T> to List<T>
    public static class EnumerableExtensions
    {
        public static List<T> AsList<T>(this IEnumerable<T> source) => new List<T>(source);
    }
}
