using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using PlanningAPI;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using PlanningAPI.Service.Dto;
using Azure;

namespace PlanningAPI.IntegrationTests
{
    public class OperatorControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public OperatorControllerIntegrationTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Get_ReturnsSuccessAndOperators()
        {
            // Act
            var response = await _client.GetAsync("/Operator");

            if (response.StatusCode != HttpStatusCode.OK)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                // Output to test log or console for debugging
                Console.WriteLine($"Error response: {errorContent}");
            }

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var operators = await response.Content.ReadFromJsonAsync<OperatorListDto[]>();
            Assert.NotNull(operators);
        }

        [Fact]
        public async Task AddLine_Works()
        {
            // Arrange
            var lineAdd = new LineAddDto(1, "INTEGRATION1");

            // Act
            var addResponse = await _client.PostAsJsonAsync("/Operator/Add/Line", lineAdd);

            if (addResponse.StatusCode != HttpStatusCode.OK)
            {
                var errorContent = await addResponse.Content.ReadAsStringAsync();
                // Output to test log or console for debugging
                Console.WriteLine($"Error response: {errorContent}");
            }
        }

        // todo other endpoints
    }
}