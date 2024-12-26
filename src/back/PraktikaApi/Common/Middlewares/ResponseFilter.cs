using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Common.Middlewares
{
    public class ResponseFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is ObjectResult objectResult)
            {
                var value = objectResult.Value;

                if (value is Task taskValue)
                {
                    value = GetTaskResult(taskValue);
                }

                if (value is not ResponseApi<object>)
                {
                    var statusCode = objectResult.StatusCode ?? StatusCodes.Status200OK;

                    if (statusCode == StatusCodes.Status200OK)
                    {
                        objectResult.Value = ResponseApi<object>.Success(value);
                    }
                }
            }
        }

        private static object GetTaskResult(Task task)
        {
            if (task.GetType().IsGenericType)
            {
                var resultProperty = task.GetType().GetProperty("Result");
                if (resultProperty != null)
                {
                    return resultProperty.GetValue(task);
                }
            }

            return null;
        }
    }
}
