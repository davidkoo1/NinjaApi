using NinjaWikiAPI.Models;

namespace NinjaWikiAPI.Dto
{
    public class SkillDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public RankDto Rank { get; set; }
    }
}
