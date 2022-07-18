using AutoMapper;
using TimeTrakAPI.Entities;
using TimeTrakAPI.Models;

namespace TimeTrakAPI.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<TimeSheet, TimeSheetDTO>();
            CreateMap<BreakTimeSheet, BreakTimeSheetDTO> ();
        }
    }
}
