using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace PagMenos.Presentation.Filters
{
	public class RequireAuthenticatedUserFilter :IAsyncActionFilter
	{
		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			if (!context.HttpContext.User.Identity?.IsAuthenticated ?? false)
			{
				context.Result = new UnauthorizedResult();
				return;
			}

			await next();
		}
	}
}
