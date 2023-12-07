using System.ComponentModel.DataAnnotations;

namespace NinjaWikiAPI.Models
{
    public class Battle
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public virtual IList<NinjaBattles> NinjaBattles { get; set; }
    }
}
