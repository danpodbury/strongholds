using StrongholdsAPI.Models;
using SimpleHashing;

namespace StrongholdsAPI.Data
{
    public static class SeedData
    {
        public static async void Init(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<StrongholdsContext>();

            var token = Guid.NewGuid().ToString();

            context.Logins.AddRange(

                new Login
                {
                    LoginID = 1,
                    Username = "admin",
                    Token = token,
                    HashedToken = PBKDF2.Hash(token),
                },
                new Login
                {
                    LoginID = 2,
                    Username = "test",
                    Token = token,
                    HashedToken = PBKDF2.Hash(token),
                }

            );

            context.Robots.AddRange(
                new Robot
                {
                    Name = "simon",
                    LoginID = 1
                },
                new Robot
                {
                    Name = "dave",
                    LoginID = 1
                },
                new Robot
                {
                    Name = "casper",
                    LoginID = 2
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