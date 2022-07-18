using TimeTrakAPI.Entities;

namespace TimeTrakAPI.Models
{
    public class BreakTimeSheetDTO
    {
            public int? Id { get; set; }
            public int? Amount { get; set; }
            public int? TimeSheetId { get; set; }
            public TimeType? TimeType { get; set; }
    }
}
