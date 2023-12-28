namespace NinjaWikiAPI.Dto
{
    public class SkillUpdateDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int RankId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
