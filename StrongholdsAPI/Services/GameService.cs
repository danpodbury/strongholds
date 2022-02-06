using StrongholdsAPI.Data;
using StrongholdsAPI.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace StrongholdsAPI.Services;

public class GameService : BackgroundService
{
    private readonly IServiceProvider _services;
    private readonly ILogger<GameService> _logger;
       
    private const int tickFrequency = 10;               // time between ticks
    private const int tickRate = 60 / tickFrequency;    // updates per min    

    private int totalTicks = 0;
    private DateTime serverStartTime = DateTime.Now;

    public GameService(IServiceProvider services, ILogger<GameService> logger)
    {
        _services = services;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        // Game Init
        InitaliseGame();

        // Game Loop
        while (!cancellationToken.IsCancellationRequested)
        {
            totalTicks++;

            // Tick
            var tickstart = DateTime.Now;
            GameLoop();
            var loopLength = (DateTime.Now - tickstart).TotalMilliseconds;
            _logger.LogInformation($"Gameloop took {loopLength} milliseconds.");

            var delay = Math.Max(0, ((tickFrequency * 1000) - loopLength));

            // Delay
            _logger.LogInformation($"waiting  {delay} milliseconds before next loop");
            await Task.Delay(TimeSpan.FromMilliseconds(delay), cancellationToken);

        }
            
    }

    private async Task<int> InitaliseGame()
    {
        return 0;
    }

    private void GameLoop()
    {
        // TODO: Updating the DB directly will probably scale poorly
        // Will interacting with the API be significantly slower?

        var dbStartTime = DateTime.Now;

        using (var scope = _services.CreateScope()) { 
            var context = scope.ServiceProvider.GetService<StrongholdsContext>();

            foreach (Robot r in context.Robots)
            {
                r.step();
            }

            context.SaveChanges();
        }

        var dbUpdateTime = (DateTime.Now - dbStartTime).TotalMilliseconds;

        _logger.LogInformation($"It took =={dbUpdateTime}== milliseconds to update the DB");
    }

}
