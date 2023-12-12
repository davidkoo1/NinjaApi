namespace NinjaWikiAPI.Dto
{
    public class ClanNinjasDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<NinjaDto> Ninjas { get; set; }
    }
}
