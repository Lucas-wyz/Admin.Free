using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Free.Filter
{
    /// <summary>
    /// 全局异常筛选
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="env"></param>
    public class GlobalExceptionFilterAttribute(ILogger<GlobalExceptionFilterAttribute> logger, IHostEnvironment env) : ExceptionFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            await Task.Delay(0);
            var error = context.Exception;
            object? data = env.IsProduction() ? null : error;
            logger.LogError(error, error.Message);
            context.Result = new OkObjectResult(new
            {
                code = 500,
                message = error.Message,
                data,
            });

        }
    }







}
