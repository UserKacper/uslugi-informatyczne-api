using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = "Server=127.0.0.1;Port=5432;Database=Cluster01;User Id=postgres;Password=postgres;";

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddDbContext<DataBaseApiContext>(opt => opt.UseNpgsql(connectionString));
builder.Services.AddScoped<ISoftwareRepository, SoftwareRepository>();
builder.Services.AddScoped<IDynamicWPRepository, DynamicWPRepository>();
builder.Services.AddScoped<IStaticWPRepository, StaticWPRepository>();
builder.Services.AddScoped<ISmartHomeRepository, SmartHomeRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options => options.AllowAnyOrigin());

app.UseHttpsRedirection();
app.MapControllers();


app.Run();


