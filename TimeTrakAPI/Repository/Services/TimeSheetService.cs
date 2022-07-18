using AutoMapper;
using TimeTrakAPI.Context;
using TimeTrakAPI.Entities;
using TimeTrakAPI.Helpers;
using TimeTrakAPI.Models;
using TimeTrakAPI.Repository.Contract;

namespace TimeTrakAPI.Repository.Services
{
    public class TimeSheetService : ITimeSheetService
    {
        private readonly TimeTrakContext _context;
        private readonly IMapper _mapper;

        public TimeSheetService(TimeTrakContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TimeSheetDTO>> GetTimeSheetByUserId()
        {
            try
            {
                List<BreakTimeSheetDTO> btsList = new List<BreakTimeSheetDTO>();
                var listTimeSheets = await Task.Run(() => _context.TimeSheets.Where(ts => ts.UserId == UserContext.UserData.Id).Select(ts => new TimeSheetDTO
                {
                    Id = ts.Id,
                    ClockIn = ts.ClockIn,
                    ClockOut = ts.ClockOut,
                    User = ts.User,
                    BreakTimeSheets = new List<BreakTimeSheetDTO>()
                }).ToList());

                listTimeSheets.ForEach(ts =>
                {
                    ts.BreakTimeSheets = _context.BreakTimeSheets.Where(bts => bts.TimeSheetId == ts.Id).Select(bts => new BreakTimeSheetDTO
                    {
                        Id = bts.Id,
                        Amount = bts.Amount,
                        TimeSheetId = bts.TimeSheetId,
                        TimeType = bts.TimeType
                    }).ToList();
                });

                return listTimeSheets.OrderByDescending(ts => ts.ClockIn);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<TimeSheetDTO>> GetTimeSheetByRangeDate(DateTime start, DateTime end)
        {
            List<TimeSheetDTO> listRange = new List<TimeSheetDTO>();
            try
            {
                listRange = await Task.Run(()=> 
                    _context.TimeSheets.Where(ts => ts.ClockIn >= start.ToUniversalTime() && ts.ClockOut <= end.AddDays(1).ToUniversalTime()).Select(ts => new TimeSheetDTO
                    {
                        Id = ts.Id,
                        ClockIn = ts.ClockIn,
                        ClockOut = ts.ClockOut,
                        User = ts.User,
                        BreakTimeSheets = new List<BreakTimeSheetDTO>()
                    }).ToList()
                );

                listRange.ForEach(ts =>
                {
                    ts.BreakTimeSheets = _context.BreakTimeSheets.Where(bts => bts.TimeSheetId == ts.Id).Select(bts => new BreakTimeSheetDTO
                    {
                        Id = bts.Id,
                        Amount = bts.Amount,
                        TimeSheetId = bts.TimeSheetId,
                        TimeType = bts.TimeType
                    }).ToList();
                });

                return listRange.OrderByDescending(ts => ts.ClockIn);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<TimeSheetDTO> AddClockIn()
        {
            try
            {
                var clockIn = new TimeSheet { ClockIn = DateTime.UtcNow, ClockOut = null, UserId = UserContext.UserData.Id };
                await _context.TimeSheets.AddAsync(clockIn);
                await _context.SaveChangesAsync();
                var tsDTO = _mapper.Map<TimeSheetDTO>(clockIn);
                return tsDTO;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<TimeSheetDTO> AddClockOut()
        {
            try
            {
                var clockOut = _context.TimeSheets.Where(x=>x.UserId == UserContext.UserData.Id && x.ClockOut==null).OrderBy(x=>x.ClockIn).LastOrDefault();
                if (clockOut != null)
                {
                    clockOut.ClockOut = DateTime.UtcNow;
                    await _context.SaveChangesAsync();
                    var tsDTO = _mapper.Map<TimeSheetDTO>(clockOut);
                    return tsDTO;
                }
                return new TimeSheetDTO();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<BreakTimeSheetDTO> AddBreake(BreakTimeSheetDTO btsDTO)
        {
            try
            {
                var tsID = _context.TimeSheets.Where(x => x.ClockOut == null && x.UserId == UserContext.UserData.Id).FirstOrDefault().Id;
                var bts = new BreakTimeSheet { Amount = btsDTO.Amount.Value, TimeSheetId = tsID, TimeTypeId = btsDTO.TimeType.Id};
                await _context.BreakTimeSheets.AddAsync(bts);
                await _context.SaveChangesAsync();
                return _mapper.Map<BreakTimeSheetDTO>(bts);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
