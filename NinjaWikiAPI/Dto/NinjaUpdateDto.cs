namespace NinjaWikiAPI.Dto
{
    public class NinjaUpdateDto
    {
        public int Id { get; set; }
        public int ClanId { get; set; }
        public int RankId { get; set; }
        public int VillageId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsTraitor { get; set; }
    }
}
