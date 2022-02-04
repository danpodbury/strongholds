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

    private long totalTicks = 0;
    private DateTime serverStartTime = DateTime.Now;

    public GameState GameState;

    public GameService(IServiceProvider services, ILogger<GameService> logger)
    {
        _services = services;
        _logger = logger;
        //_clientFactory = clientFactory;
        //_context = context;
        //GameState = new GameState();
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
        try
        {
            using (var scope = _services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<StrongholdsContext>();

                // Initalise gamestate from DB
                //var Robots = await context.Robots.ToListAsync();
                //foreach (Robot r in Robots)
                //{
                //    GameState.Robots.Add(r);
                //}
                //var Locations = await context.Locations.ToListAsync();
                //foreach (Location l in Locations)
                //{
                //    GameState.Locations.Add(l);
                //}
            }
            return 0;
        }
        catch
        {
            _logger.LogCritical("Game failed to initalise from DB");
            return 1;
        }

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
                //context.Robots.Update(r);
                if (r.RobotID % 2 == 0)
                {
                    r.latitude += 0.001f;
                    r.longitude += 0.001f;
                }
                else
                {
                    r.latitude -= 0.001f;
                    r.longitude -= 0.001f;
                }
            }


            context.SaveChanges();
        
        }

        var dbUpdateTime = (DateTime.Now - dbStartTime).TotalMilliseconds;

        _logger.LogInformation($"It took {dbUpdateTime} milliseconds to update the DB");
    }

}
