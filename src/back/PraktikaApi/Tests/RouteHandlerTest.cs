using Moq;
using PraktikaApplication.RouteHandlers.RouteCommand;
using PraktikaApplication.RouteHandlers.RouteHandler;
using PraktikaDomain.Interfaces;

namespace RouteHandlerTests
{
    public class RouteHandlerTests
    {
        private readonly Mock<IRouteService> _mockRouteService;
        private readonly CalculateRouteHandler _calculateRouteHandler;

        public RouteHandlerTests()
        {
            _mockRouteService = new Mock<IRouteService>();
            _calculateRouteHandler = new CalculateRouteHandler(_mockRouteService.Object);
        }

        [Fact]
        public async Task MinskGrodno()
        {
            // Arrange
            var command = new CalculateRouteCommand
            {
                StartPointName = "Minsk",
                EndPointName = "Grodno"
            };

            var startCoordinates = (Latitude: 53.9045, Longitude: 27.5558);
            var endCoordinates = (Latitude: 53.6694, Longitude: 23.8131);

            var expectedRoute = "{ \"routes\": [{ \"summary\": \"Route from Minsk to Grodno\" }] }";

            _mockRouteService.Setup(service => service.GetCoordinates(command.StartPointName)).ReturnsAsync(startCoordinates);
            _mockRouteService.Setup(service => service.GetCoordinates(command.EndPointName)).ReturnsAsync(endCoordinates);
            _mockRouteService.Setup(service => service.Gps(It.Is<double[]>(sp => sp[0] == startCoordinates.Longitude && sp[1] == startCoordinates.Latitude), It.Is<double[]>(ep => ep[0] == endCoordinates.Longitude && ep[1] == endCoordinates.Latitude)))
                             .ReturnsAsync(expectedRoute);

            // Act
            var result = await _calculateRouteHandler.Handle(command, CancellationToken.None);
            // Assert
            Assert.Equal(expectedRoute, result);
        }
    }
}
