using Microsoft.EntityFrameworkCore;
using PlanningAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

            modelBuilder.Entity<Line>().HasIndex(x => x.OperatorNo);
            modelBuilder.Entity<Trip>().HasIndex(x => x.LineNo);
            modelBuilder.Entity<UpdateLog>().HasIndex(x => x.TripNo);

            // data seeding
            modelBuilder.Entity<Operator>().HasData(
                new { OperatorId = 1, Name = "HTM", ApiEndpoint = "api.htm.net" },
                new { OperatorId = 2, Name = "Arriva", ApiEndpoint = "api.arriva.nl" }
            );

            modelBuilder.Entity<Line>().HasData(
                new { LineId = 1, OperatorNo = 1, LinePlanningNumber = "Bus 24" },
                new { LineId = 2, OperatorNo = 1, LinePlanningNumber = "Tram 12" }
            );

            modelBuilder.Entity<Trip>().HasData(
                new { TripId = 1, LineNo = 2, DepartureTime = DateTime.Now.AddHours(-1), ArrivalTime = DateTime.Now.AddMinutes(30) },
                new { TripId = 2, LineNo = 2, DepartureTime = DateTime.Now.AddMinutes(15), ArrivalTime = DateTime.Now.AddMinutes(45) }
            );
        }
    }
}
