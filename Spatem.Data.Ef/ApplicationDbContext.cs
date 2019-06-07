using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Spatem.Core.Models;
using Spatem.Data.Ef.Configurations;

namespace Spatem.Data.Ef
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new WalletConfiguration());
            builder.ApplyConfiguration(new TransactionConfiguration());
            base.OnModelCreating(builder);
        }
    }
}