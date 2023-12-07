using System.ComponentModel.DataAnnotations;

namespace NinjaWikiAPI.Models
{
    public class Clan
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public virtual IList<Ninja> Ninjas { get; set; } 
    }
}
