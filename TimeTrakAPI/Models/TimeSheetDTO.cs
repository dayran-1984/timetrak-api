using TimeTrakAPI.Entities;

namespace TimeTrakAPI.Models
{
    public class TimeSheetDTO
    {
        public int Id { get; set; }
        public DateTime ClockIn { get; set; }
        public DateTime? ClockOut { get; set; }
        public User User { get; set; }
        public List<BreakTimeSheetDTO> BreakTimeSheets { get; set; }
    }
}
