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

            //CreateMap<Ninja, NinjaUpdateDto>();
            CreateMap<NinjaUpdateDto, Ninja>();

            CreateMap<Ninja, NinjaInfoDto>();
            //CreateMap<NinjaClanDto, Ninja>();


            CreateMap<Rank, RankDto>();
            CreateMap<RankDto, Rank>();

            CreateMap<Village, VillageDto>();
            CreateMap<VillageDto, Village>();

            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();


            CreateMap<Skill, SkillDto>();
            CreateMap<SkillDto, Skill>();

        }
    }
}
