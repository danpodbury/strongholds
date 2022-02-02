using StrongholdsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace StrongholdsAPI.Data
{
    public class StrongholdsContext : DbContext
    {
        public StrongholdsContext(DbContextOptions<StrongholdsContext> options) : base(options)
        { }

        public DbSet<Robot> Robots { get; set; }
        public DbSet<Location> Locations { get; set; }
        //public DbSet<Transaction> Transactions { get; set; }
        //public DbSet<Login> Logins { get; set; }
        //public DbSet<Payee> Payees { get; set; }
        //public DbSet<BillPay> BillPays { get; set; }
    }
}
