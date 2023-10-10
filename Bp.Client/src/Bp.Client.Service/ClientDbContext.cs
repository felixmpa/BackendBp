using Bp.Client.Service.Entities;
using Bp.Common;
using Microsoft.EntityFrameworkCore;

namespace Bp.Client
{
    public class ClientDbContext : CommonDbContext
    {
        public ClientDbContext(DbContextOptions<ClientDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Additional configurations for ClientDbContext

            modelBuilder.Entity<Customer>()
                .HasOne(c => c.Person)
                .WithOne()
                .HasForeignKey<Customer>(c => c.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Person>()
                .Property(p => p.Sex)
                .HasConversion<string>()
                .HasMaxLength(50);

        }
    }
}
