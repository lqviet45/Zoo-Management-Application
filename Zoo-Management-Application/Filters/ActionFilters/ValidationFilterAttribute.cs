using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace Zoo.Management.Application.Filters.ActionFilters
{
	public class ValidationFilterAttribute : IAsyncActionFilter
	{
		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			foreach (var argument in context.ActionArguments.Values)
			{
				if (argument is null)
				{
					context.Result = new BadRequestObjectResult("The request object is null.");
					return;
				}

				if (!context.ModelState.IsValid)
				{
					context.Result = new BadRequestObjectResult(context.ModelState);
					return;
				}
			}

			await next(); // Continue to the action method
		}
	}
}
