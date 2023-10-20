using Entities.AppDbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Zoo.Management.Application.Filters.ActionFilters
{
	public class ValidateEntityExistsAttribute<T> : IAsyncActionFilter where T : class
	{
		private readonly ApplicationDbContext _context;
		private readonly string _keyName;
		private readonly Type _keyType;

		public ValidateEntityExistsAttribute(ApplicationDbContext context, string keyName, Type keyType)
		{
			_context = context;
			_keyName = keyName;
			_keyType = keyType;
		}

		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{

			if (!_context.Model.FindEntityType(typeof(T)).GetProperties().Any(p => p.Name == _keyName))
			{
				context.Result = new BadRequestObjectResult("Invalid key name.");
				return;
			}

			if (context.ActionArguments.ContainsKey(_keyName))
			{
				var keyValue = context.ActionArguments[_keyName];

				if (keyValue != null && _keyType.IsInstanceOfType(keyValue))
				{
					var parameter = Expression.Parameter(typeof(T));
					var property = Expression.Property(parameter, _keyName);
					var key = Expression.Constant(keyValue);
					var equals = Expression.Equal(property, key);
					var lambda = Expression.Lambda<Func<T, bool>>(equals, parameter);

					var entity = await _context.Set<T>().Where(lambda).FirstOrDefaultAsync();

					if (entity == null)
					{
						context.Result = new NotFoundResult();
					}
					else
					{
						context.HttpContext.Items.Add("entity", entity);
					}
				}
				else
				{
					context.Result = new BadRequestObjectResult($"Bad {_keyName} parameter");
				}
			}
			else
			{
				context.Result = new BadRequestObjectResult($"No {_keyName} parameter");
			}

			await next();
		}
	}
}