using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
var appInit = new AppInitilization(builder.Configuration);
string dbConnection = await appInit.AppInit("db");
Console.WriteLine(dbConnection);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddDbContext<DataBaseApiContext>(opt => opt.UseNpgsql(dbConnection));
builder.Services.AddScoped<IPricingRepository, PricingRepository>();
builder.Services.AddScoped<IEmailValidation, EmailValidation>();
builder.Services.AddScoped<IAppInitization, AppInitilization>();
builder.Services.AddMemoryCache();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});
var app = builder.Build();

app.UseCors();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API v1");
    });
}

app.UseHttpsRedirection();
app.MapControllers();
app.UseMiddleware<IpAddressMiddleware>();
app.UseRouting();

if (app.Environment.IsDevelopment() && !app.Environment.IsProduction())
{
    app.Run();
}
else
{
    app.Run("http://0.0.0.0:80");
}
