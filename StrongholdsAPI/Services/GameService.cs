using StrongholdsAPI.Data;
using StrongholdsUtil.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace StrongholdsAPI.Services;

public class GameService : BackgroundService
{
    private readonly IServiceProvider _services;
    private readonly ILogger<GameService> _logger;
       
    public const int tickPeriod = 10;               // time between ticks

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

            var delay = Math.Max(0, ((tickPeriod * 1000) - loopLength));

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
            var missions = context.Missions.ToList();
            foreach (Mission m in missions)
            {

                // move robot to next objective
                m.Robot.step(m);

                // end remove Mission from DB if no objectives left
                if (m.Objectives.Count == 0)
                {
                    //context.Missions.Where(i => i.RobotID == m.RobID).ToList();
                    context.Missions.Remove(context.Missions.Where(i => i.RobotID == m.RobotID).ToList()[0]);
                }

            }

            // Push changes to DB
            context.SaveChanges();
        }

        var dbUpdateTime = (DateTime.Now - dbStartTime).TotalMilliseconds;

        _logger.LogInformation($"It took =={dbUpdateTime}== milliseconds to update the DB");
    }

}
