using Bp.Transaction.Service.Entities;
using Bp.Common;
using Microsoft.EntityFrameworkCore;

namespace Bp.Transaction
{
    public class TransactionDbContext : CommonDbContext
    {
        public TransactionDbContext(DbContextOptions<TransactionDbContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Additional configurations for TransactionDbContext

            modelBuilder.Entity<TransactionBalance>()
                .HasOne(t => t.Account)
                .WithMany()
                .HasForeignKey(t => t.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
