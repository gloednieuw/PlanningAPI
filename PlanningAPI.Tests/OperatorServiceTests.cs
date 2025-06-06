using Xunit;
using Moq;
using PlanningAPI.Model;
using PlanningAPI.Service;
using System.Threading.Tasks;
using System.Collections.Generic;
using PlanningAPI.Service.Dto;

namespace PlanningAPI.Tests
{
    public class OperatorServiceTests
    {
        [Fact]
        public async Task GetAllOperators_ReturnsOperatorListDtos()
        {
            // Arrange
            var mockRepo = new Mock<IRepository<Operator>>();
            var operators = new List<Operator>
            {
                new Operator(1, "Test1", "api1"),
                new Operator(2, "Test2", "api2")
            };
            mockRepo.Setup(r => r.ListAsync(null)).ReturnsAsync(operators);
            var service = new OperatorService(mockRepo.Object);

            // Act
            var result = await service.GetAllOperators();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(result, o => o.OperatorId == 1 && o.Name == "Test1");
            Assert.Contains(result, o => o.OperatorId == 2 && o.Name == "Test2");
        }

        [Fact]
        public async Task GetOperatorDetails_ReturnsOperatorViewDto_WhenOperatorExists()
        {
            // Arrange
            var mockRepo = new Mock<IRepository<Operator>>();
            var operatorEntity = new Operator(1, "Test", "api");
            mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(operatorEntity);
            var service = new OperatorService(mockRepo.Object);

            // Act
            var result = await service.GetOperatorDetails(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.OperatorId);
        }

        [Fact]
        public async Task GetOperatorDetails_ThrowsArgumentException_WhenOperatorDoesNotExist()
        {
            // Arrange
            var mockRepo = new Mock<IRepository<Operator>>();
            mockRepo.Setup(r => r.GetByIdAsync(2)).ReturnsAsync((Operator)null);
            var service = new OperatorService(mockRepo.Object);

            await Assert.ThrowsAsync<ArgumentException>(() => service.GetOperatorDetails(2));
        }

        [Fact]
        public async Task AddLine_ReturnsTrue_WhenOperatorExists()
        {
            // Arrange
            var mockRepo = new Mock<IRepository<Operator>>();
            var op = new Operator(1, "Test", "api");
            mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(op);
            mockRepo.Setup(r => r.UpdateAsync(op)).ReturnsAsync(true);
            var service = new OperatorService(mockRepo.Object);

            var dto = new LineAddDto(1, "ABC123");

            // Act
            var result = await service.AddLine(dto);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task AddLine_ThrowsArgumentException_WhenOperatorDoesNotExist()
        {
            // Arrange
            var mockRepo = new Mock<IRepository<Operator>>();
            mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Operator)null);
            var service = new OperatorService(mockRepo.Object);

            var dto = new LineAddDto(1, "ABC123");

            await Assert.ThrowsAsync<ArgumentException>(() => service.AddLine(dto));
        }

        [Fact]
        public async Task AddTripToLine_ReturnsTrue_WhenOperatorAndLineExist()
        {
            // Arrange
            var mockRepo = new Mock<IRepository<Operator>>();
            var line = new Line(1, 1, "ABC123");
            var op = new Operator(1, "Test", "api");
            op.AddLine(line);
            mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(op);
            mockRepo.Setup(r => r.UpdateAsync(op)).ReturnsAsync(true);
            var service = new OperatorService(mockRepo.Object);

            var dto = new TripAddDto
            {
                OperatorId = 1,
                LineId = line.LineId,
                DepartureTime = System.DateTime.UtcNow.AddHours(1),
                ArrivalTime = System.DateTime.UtcNow.AddHours(2)
            };

            // Act
            var result = await service.AddTripToLine(dto);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task AddTripToLine_ThrowsArgumentException_WhenOperatorDoesNotExist()
        {
            // Arrange
            var mockRepo = new Mock<IRepository<Operator>>();
            mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Operator)null);
            var service = new OperatorService(mockRepo.Object);

            var dto = new TripAddDto
            {
                OperatorId = 1,
                LineId = 1,
                DepartureTime = System.DateTime.UtcNow.AddHours(1),
                ArrivalTime = System.DateTime.UtcNow.AddHours(2)
            };

            await Assert.ThrowsAsync<ArgumentException>(() => service.AddTripToLine(dto));
        }

        [Fact]
        public async Task AddTripToLine_ThrowsArgumentException_WhenLineDoesNotExist()
        {
            // Arrange
            var mockRepo = new Mock<IRepository<Operator>>();
            var op = new Operator(1, "Test", "api");
            mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(op);
            var service = new OperatorService(mockRepo.Object);

            var dto = new TripAddDto
            {
                OperatorId = 1,
                LineId = 999, // Non-existent line
                DepartureTime = System.DateTime.UtcNow.AddHours(1),
                ArrivalTime = System.DateTime.UtcNow.AddHours(2)
            };

            await Assert.ThrowsAsync<ArgumentException>(() => service.AddTripToLine(dto));
        }
    }
}