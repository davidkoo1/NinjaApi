using AutoMapper;
using NinjaWikiAPI.Dto;
using NinjaWikiAPI.Models;

namespace NinjaWikiAPI.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Clan, ClanNinjasDto>();
            //CreateMap<ClanNinjasDto, Clan>();

            CreateMap<Clan, ClanDto>();
            CreateMap<ClanDto, Clan>();

            CreateMap<Ninja, NinjaDto>();
            CreateMap<NinjaDto, Ninja>();

            CreateMap<Ninja, NinjaClanDto>();
            //CreateMap<NinjaClanDto, Ninja>();
        }
    }
}
