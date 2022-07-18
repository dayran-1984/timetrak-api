using Microsoft.EntityFrameworkCore;
using TimeTrakAPI.Entities;

namespace TimeTrakAPI.Context
{
    public class TimeTrakContext :  DbContext
    {
        public TimeTrakContext(DbContextOptions<TimeTrakContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<TimeSheet> TimeSheets { get; set; }
        public DbSet<BreakTimeSheet> BreakTimeSheets { get; set; }
        public DbSet<TimeType> TimeTypes { get; set; }
    }
}
