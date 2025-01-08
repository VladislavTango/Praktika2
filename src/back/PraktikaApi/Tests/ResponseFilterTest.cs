using CommonShared.Middlewares;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Moq;

namespace ResponseFilterTests
{
    public class ResponseFilterTests
    {
        [Fact]
        public void OnActionExecuted_ShouldWrapResponseApi_ForObjectResult()
        {
            // Arrange
            var responseFilter = new ResponseFilter();
            var objectResult = new ObjectResult("Test Data")
            {
                StatusCode = StatusCodes.Status200OK
            };
            var context = new ActionExecutedContext(
                new ActionContext
                {
                    HttpContext = new DefaultHttpContext(),
                    RouteData = new Microsoft.AspNetCore.Routing.RouteData(),
                    ActionDescriptor = new Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor()
                },
                new IFilterMetadata[0],
                new Mock<Controller>().Object);

            context.Result = objectResult;

            // Act
            responseFilter.OnActionExecuted(context);

            // Assert
            Assert.IsType<ResponseApi<object>>(objectResult.Value);
            var apiResponse = (ResponseApi<object>)objectResult.Value;
            Assert.True(apiResponse.Successed);
            Assert.Equal("Test Data", apiResponse.Response);
        }

        [Fact]
        public void OnActionExecuted_ShouldHandleTaskResult()
        {
            // Arrange
            var responseFilter = new ResponseFilter();
            var task = Task.FromResult("Test Data");
            var objectResult = new ObjectResult(task)
            {
                StatusCode = StatusCodes.Status200OK
            };
            var context = new ActionExecutedContext(
                new ActionContext
                {
                    HttpContext = new DefaultHttpContext(),
                    RouteData = new Microsoft.AspNetCore.Routing.RouteData(),
                    ActionDescriptor = new Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor()
                },
                new IFilterMetadata[0],
                new Mock<Controller>().Object);

            context.Result = objectResult;

            // Act
            responseFilter.OnActionExecuted(context);

            // Assert
            Assert.IsType<ResponseApi<object>>(objectResult.Value);
            var apiResponse = (ResponseApi<object>)objectResult.Value;
            Assert.True(apiResponse.Successed);
            Assert.Equal("Test Data", apiResponse.Response);
        }
    }
}
