using Toolkit_API.Middleware.Exceptions;

namespace Toolkit_API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        public ExceptionMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (AppException apex)
            {
                context.Response.StatusCode = apex.StatusCode;
                await context.Response.WriteAsJsonAsync(new
                {
                    message = apex.Message
                });
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new
                {
                    message = "Internal server error"
                });
            }
        }
    }
}
