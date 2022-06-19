using PublishingHouse.Interfaces.Model;

namespace PublishingHouse.Middlewares;

/// <summary>
///     Промежуточное ПО для обработки исключений
/// </summary>
public class ExceptionMiddleware
{
	private readonly RequestDelegate _next;

	public ExceptionMiddleware(RequestDelegate next)
	{
		_next = next;
	}

	/// <summary>
	///     Вызов
	/// </summary>
	/// <param name="context"></param>
	public async Task InvokeAsync(HttpContext context)
	{
		try
		{
			await _next.Invoke(context);
		}
		catch (Exception e)
		{
			await context.Response.WriteAsJsonAsync(new BaseResponse(e));
			context.Response.StatusCode = 500;
		}
	}
}