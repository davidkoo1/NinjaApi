using System.ComponentModel.DataAnnotations;

namespace NinjaWikiAPI.Models
{
    public class Battle
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public virtual IEnumerable<NinjaBattles> NinjaBattles { get; set; }
    }
}
