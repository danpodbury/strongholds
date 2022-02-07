using StrongholdsAPI.Data;
using StrongholdsAPI.Managers;
using StrongholdsAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore.InMemory;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<StrongholdsContext>(options =>
{
        //options.UseInMemoryDatabase("Main");
        options.UseSqlServer(builder.Configuration.GetConnectionString("StrongholdsContext"));
        options.UseLazyLoadingProxies();
});

// Store session into Web-Server memory.
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => options.Cookie.IsEssential = true );

// Set up repository managers
builder.Services.AddScoped<RobotManager>();
builder.Services.AddScoped<LocationManager>();
builder.Services.AddScoped<LoginManager>();
builder.Services.AddScoped<StationManager>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add background services
builder.Services.AddHostedService<GameService>();

var app = builder.Build();

// Run startup tasks that require scoped services
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        // Seed fake database
        SeedData.Init(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
