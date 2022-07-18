using Microsoft.AspNetCore.Mvc;
using TimeTrakAPI.Helpers;
using TimeTrakAPI.Models;
using TimeTrakAPI.Repository.Contract;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TimeTrakAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class TimeSheetController : ControllerBase
    {
        private ITimeSheetService _timeSheetService;

        public TimeSheetController(ITimeSheetService timeSheetService)
        {
            _timeSheetService = timeSheetService;
        }

        // GET: api/<TimeSheetController>
        [HttpGet]
        public async Task<ActionResult> GetTimeSheetByUserId()
            {
            return Ok(await _timeSheetService.GetTimeSheetByUserId());
        }

        [HttpGet("{start}/{end}")]
        public async Task<ActionResult> GetTimeSheetByRangeDate(DateTime start, DateTime end)
        {
            return Ok(await _timeSheetService.GetTimeSheetByRangeDate(start,end));
        }

        [HttpPost]
        public async Task<ActionResult> AddClockIn()
        {
            return Ok(await _timeSheetService.AddClockIn());
        }

        [HttpPost]
        public async Task<ActionResult> AddClockOut()
        {
            return Ok(await _timeSheetService.AddClockOut());
        }

        [HttpPost]
        public async Task<ActionResult> AddBreake([FromBody] BreakTimeSheetDTO btsDTO)
        {
            return Ok(await _timeSheetService.AddBreake(btsDTO));
        }

        //// GET api/<TimeSheetController>/5
        //[HttpGet("{userId}")]
        //public IEnumerable<TimeSheetDTO> Get(int userId)
        //{
        //    return this._timeSheetService.GetTimeSheetByUserId(userId);
        //}

        // POST api/<TimeSheetController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<TimeSheetController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<TimeSheetController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
