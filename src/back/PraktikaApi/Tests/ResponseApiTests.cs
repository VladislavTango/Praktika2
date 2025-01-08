using CommonShared.Middlewares;
namespace ResponseApiTests
{
    public class ResponseApiTests
    {
        [Fact]
        public void Success_ShouldReturnSuccessResponse()
        {
            // Arrange
            var data = "Test Data";

            // Act
            var response = ResponseApi<string>.Success(data);

            // Assert
            Assert.True(response.Successed);
            Assert.Equal(data, response.Response);
        }

        [Fact]
        public void Fail_ShouldReturnFailResponse()
        {
            // Arrange
            var errorMessage = "Error occurred";

            // Act
            var response = ResponseApi<string>.Fail(errorMessage);

            // Assert
            Assert.False(response.Successed);
            Assert.Equal(errorMessage, response.Response);
        }
    }
}