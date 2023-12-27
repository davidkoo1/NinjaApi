namespace NinjaWikiAPI.Dto
{
    public class NinjaInfoDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsTraitor { get; set; }


        public virtual ClanDto Clan { get; set; }
        public virtual RankDto Rank { get; set; }
        public virtual VillageDto Village { get; set; }
        public virtual List<SkillDto> Skills { get; set; }
    }
}
