using StrongholdsUtil.Models;
using SimpleHashing;

namespace StrongholdsAPI.Data
{
    public static class SeedData
    {
        public static async void Init(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<StrongholdsContext>();

            // Check if DB is seeded
            if (context.Logins.Any())
            {
                return; // DB has already been seeded.
            }

            Console.WriteLine("Seeding database");

            context.Logins.Add(
                new Login
                {
                    Username = "admin",
                    Token = "admin",
                    HashedToken = PBKDF2.Hash("admin"),
                }
            );

            var token = Guid.NewGuid().ToString();

            context.Logins.Add(
                new Login
                {
                    Username = "dave",
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
                    Name = "greg",
                    LoginID = 1
                },
                new Robot
                {
                    Name = "casper",
                    LoginID = 1
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