using AutoMapper;
using NinjaWikiAPI.Dto;
using NinjaWikiAPI.Models;

namespace NinjaWikiAPI.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Clan, ClanDto>();
            CreateMap<ClanDto, Clan>();


        }
    }
}
