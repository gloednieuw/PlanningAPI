using Microsoft.EntityFrameworkCore;
using PlanningAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanningAPI.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Operator> Operators { get; set; }
        public DbSet<Line> Lines { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<UpdateLog> UpdateLogs { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Operator>().HasKey(x => x.OperatorId);
            modelBuilder.Entity<Line>().HasKey(x => x.LineId);
            modelBuilder.Entity<Trip>().HasKey(x => x.TripId);
            modelBuilder.Entity<UpdateLog>().HasKey(x => x.UpdateLogId);

            modelBuilder.Entity<Operator>().HasMany(x => x.Lines).WithOne().HasForeignKey(x => x.OperatorNo).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Line>().HasMany(x => x.Trips).WithOne().HasForeignKey(x => x.LineNo).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Trip>().HasMany(x => x.UpdateLogs).WithOne().HasForeignKey(x => x.TripNo).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
