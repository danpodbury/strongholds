using StrongholdsAPI.Models;

namespace StrongholdsAPI.Data
{
    public static class SeedData
    {
        public static async void Init(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<StrongholdsContext>();
            
            context.Robots.AddRange(
                new Robot
                {
                    Name = "simon",
                    Coordinates = new()
                },
                new Robot
                {
                    Name = "dave",
                    Coordinates = new()
                },
                new Robot
                {
                    Name = "casper",
                    Coordinates = new()
                }
            );

            context.Locations.AddRange(
                new Location
                {
                    Name = "Outpost 1",
                    Coordinates = new()
                },
                new Location
                {
                    Name = "Rocky outcrop",
                    Coordinates = new()
                },
                new Location
                {
                    Name = "crater",
                    Coordinates = new()
                }
            );

            context.SaveChanges();

        }
    }
}