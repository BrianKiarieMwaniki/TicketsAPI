using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TicketsAPI.Filter
{
    public class APIKeyAuthFilterAttribute : Attribute, IAuthorizationFilter
    {
        public const string ApiKeyHeader = "ApiKey";

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if(!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeader, out var clientApiKey))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var config = context.HttpContext.RequestServices.GetService(typeof(IConfiguration)) as IConfiguration;
            var apiKey = config.GetValue<string>("ApiKey");

            if(apiKey != clientApiKey)
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
