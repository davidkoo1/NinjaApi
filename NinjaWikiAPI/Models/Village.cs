using System.ComponentModel.DataAnnotations;

namespace NinjaWikiAPI.Models
{
    public class Village
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public virtual IEnumerable<Ninja> Ninjas { get; set; }
        public virtual IEnumerable<Battle> Battles { get; set; }

    }
}
