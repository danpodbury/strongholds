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
                    Name = "simon"
                },
                new Robot
                {
                    Name = "dave"
                },
                new Robot
                {
                    Name = "casper"
                }
            );

            context.Locations.AddRange(
                new Location
                {
                    Name = "Outpost 1"
                },
                new Location
                {
                    Name = "Rocky outcrop"
                },
                new Location
                {
                    Name = "crater"
                }
            );

            context.SaveChanges();

        }
    }
}