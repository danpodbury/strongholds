using StrongholdsUtil.Models;
using Microsoft.EntityFrameworkCore;

namespace StrongholdsAPI.Data
{
    public class StrongholdsContext : DbContext
    {
        public StrongholdsContext(DbContextOptions<StrongholdsContext> options) : base(options)
        { }

        public DbSet<Robot> Robots { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Mission> Missions { get; set; }
        public DbSet<Objective> Objectives { get; set; }

    }
}
