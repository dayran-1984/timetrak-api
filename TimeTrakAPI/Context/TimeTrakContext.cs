using Microsoft.EntityFrameworkCore;
using TimeTrakAPI.Entities;

namespace TimeTrakAPI.Context
{
    public class TimeTrakContext :  DbContext
    {
        public TimeTrakContext(DbContextOptions<TimeTrakContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "Dayran",
                    LastName = "alv",
                    Username = "dayran",
                    Password = "r4UrWgC8gZUOa/eI1ULtow=="
                }
            );
            modelBuilder.Entity<TimeType>().HasData(
                new TimeType
                {
                    Id=1,
                    Type="Minutes"
                },
                new TimeType
                {
                    Id = 2,
                    Type = "Hours"
                }
            );
        }

        public DbSet<User> Users { get; set; }
        public DbSet<TimeSheet> TimeSheets { get; set; }
        public DbSet<BreakTimeSheet> BreakTimeSheets { get; set; }
        public DbSet<TimeType> TimeTypes { get; set; }
    }
}
