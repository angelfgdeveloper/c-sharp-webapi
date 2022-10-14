public class Time
{
  readonly RequestDelegate next;

  public Time(RequestDelegate nextRequest)
  {
    next = nextRequest;
  }

  public async Task Invoke(Microsoft.AspNetCore.Http.HttpContext context)
  {
    await next(context); // Siempre obtenemos el último middlware y despues hacemos nuestro middleware

    // code ..
    if (context.Request.Query.Any(p => p.Key == "time"))
    {
      // Devuelve la hor<
      await context.Response.WriteAsync(DateTime.Now.ToShortTimeString());
    }

    // await next(context); // No devuelve la informacion ya que viene modificado
  }

}

public static class TimeExtension
{
  public static IApplicationBuilder UseTime(this IApplicationBuilder builder)
  {
    return builder.UseMiddleware<Time>();
  }
}