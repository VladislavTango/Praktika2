using System.Net;
using CommonShared.Middlewares;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;

namespace ExceptionHandlerMiddlewareTest
{
    public class ExceptionHandlerMiddlewareTest
    {
        [Fact]
        public async Task InvokeAsync()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<ExceptionHandlerMiddleware>>();
            var middleware = new ExceptionHandlerMiddleware(async (innerHttpContext) =>
            {
                throw new Exception("error");
            }, mockLogger.Object);
            var context = new DefaultHttpContext();

            // Act
            await middleware.InvokeAsync(context);

            // Assert
            Assert.Equal((int)HttpStatusCode.InternalServerError, context.Response.StatusCode);
        }
    }

}
