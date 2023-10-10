using System;
using Microsoft.EntityFrameworkCore;

namespace Bp.Common
{
    public class CommonDbContext : DbContext
    {
        public CommonDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}