namespace TimeTrakAPI.Entities
{
    public class TimeSheet
    {
        public int Id { get; set; }
        public DateTime ClockIn { get; set; }
        public DateTime? ClockOut { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
        public virtual List<BreakTimeSheet> BreakTimeSheets { get; set; }
    }
}
