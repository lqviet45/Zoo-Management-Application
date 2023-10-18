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
				if (argument != null && argument.GetType().IsClass && argument.GetType() != typeof(string))
				{
					var properties = argument.GetType().GetProperties();
					foreach (var property in properties)
					{
						if (property.GetValue(argument) == null)
						{
							context.ModelState.AddModelError(property.Name, "Property cannot be null.");
						}
					}
				}
			}

			if (!context.ModelState.IsValid)
			{
				context.Result = new BadRequestObjectResult(context.ModelState);
				return;
			}

			await next(); // Continue to the action method
		}
	}
}
