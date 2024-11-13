public class IpAddressMiddleware
{
    private readonly RequestDelegate _next;

    public IpAddressMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var ipAddress = context.Connection.RemoteIpAddress?.ToString();

        context.Items["IpAddress"] = ipAddress;

        await _next(context);
    }
}
