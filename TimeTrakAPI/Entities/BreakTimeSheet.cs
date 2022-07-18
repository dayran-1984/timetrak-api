namespace TimeTrakAPI.Entities
{
    public class BreakTimeSheet
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public int TimeTypeId { get; set; }
        public int TimeSheetId { get; set; }

        public virtual TimeSheet TimeSheet { get; set; }

        public virtual TimeType TimeType { get; set; }
    }
}
