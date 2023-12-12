using NinjaWikiAPI.Models;

namespace NinjaWikiAPI.Dto
{
    public class NinjaDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsTraitor { get; set; }


        //public virtual ClanDto Clan { get; set; }
    }
}
