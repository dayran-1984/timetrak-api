using TimeTrakAPI.Entities;
using TimeTrakAPI.Models;

namespace TimeTrakAPI.Repository.Contract
{
    public interface ITimeSheetService
    {
        Task<IEnumerable<TimeSheetDTO>> GetTimeSheetByUserId();
        Task<IEnumerable<TimeSheetDTO>> GetTimeSheetByRangeDate(DateTime start, DateTime end);
        Task<TimeSheetDTO> AddClockIn();
        Task<TimeSheetDTO> AddClockOut();
        Task<BreakTimeSheetDTO> AddBreake(BreakTimeSheetDTO btsDTO);
    }
}
