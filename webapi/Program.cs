using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = "Server=host.docker.internal;Port=5432;Database=Cluster01;User Id=postgres;Password=postgres;";

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddDbContext<DataBaseApiContext>(opt => opt.UseNpgsql(connectionString));
builder.Services.AddScoped<IPricingRepository, PricingRepository>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:8080")
              .AllowAnyHeader()
              .AllowAnyMethod();
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

if (app.Environment.IsDevelopment() && !app.Environment.IsProduction())
{
    app.Run();
}
else
{
    app.Run("http://0.0.0.0:80");
}
