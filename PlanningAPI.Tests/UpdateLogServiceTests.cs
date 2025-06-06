using Xunit;
using Moq;
using System;
using System.Threading.Tasks;
using PlanningAPI.Service;
using PlanningAPI.Model;
using PlanningAPI.Service.Dto;

namespace PlanningAPI.Tests
{
    public class UpdateLogServiceTests
    {
        [Fact]
        public async Task AddUpdateLogEntryForTrip_ReturnsTrue_WhenTripExistsAndAddSucceeds()
        {
            // Arrange
            var mockRepo = new Mock<IRepository<UpdateLog>>();
            var mockDomainServices = new Mock<IDomainServices>();
            var updateLogDto = new UpdateLogAddDto(1, DateTime.UtcNow, UpdateLogStatus.OnTime);

            mockDomainServices.Setup(ds => ds.TripExists(updateLogDto.TripNo)).ReturnsAsync(true);
            mockRepo.Setup(r => r.AddAsync(It.IsAny<UpdateLog>())).ReturnsAsync(true);

            var service = new UpdateLogService(mockRepo.Object, mockDomainServices.Object);

            // Act
            var result = await service.AddUpdateLogEntryForTrip(updateLogDto);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task AddUpdateLogEntryForTrip_ThrowsArgumentException_WhenTripDoesNotExist()
        {
            // Arrange
            var mockRepo = new Mock<IRepository<UpdateLog>>();
            var mockDomainServices = new Mock<IDomainServices>();
            var updateLogDto = new UpdateLogAddDto(1, DateTime.UtcNow, UpdateLogStatus.OnTime);

            mockDomainServices.Setup(ds => ds.TripExists(updateLogDto.TripNo)).ReturnsAsync(false);

            var service = new UpdateLogService(mockRepo.Object, mockDomainServices.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => service.AddUpdateLogEntryForTrip(updateLogDto));
        }

        [Fact]
        public async Task AddUpdateLogEntryForTrip_ReturnsFalse_WhenRepositoryAddFails()
        {
            // Arrange
            var mockRepo = new Mock<IRepository<UpdateLog>>();
            var mockDomainServices = new Mock<IDomainServices>();
            var updateLogDto = new UpdateLogAddDto(1, DateTime.UtcNow, UpdateLogStatus.OnTime);

            mockDomainServices.Setup(ds => ds.TripExists(updateLogDto.TripNo)).ReturnsAsync(true);
            mockRepo.Setup(r => r.AddAsync(It.IsAny<UpdateLog>())).ReturnsAsync(false);

            var service = new UpdateLogService(mockRepo.Object, mockDomainServices.Object);

            // Act
            var result = await service.AddUpdateLogEntryForTrip(updateLogDto);

            // Assert
            Assert.False(result);
        }
    }
}
