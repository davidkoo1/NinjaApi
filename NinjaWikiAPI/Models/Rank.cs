using System.ComponentModel.DataAnnotations;

namespace NinjaWikiAPI.Models
{
    public class Rank
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }

        public virtual IEnumerable<Ninja> Ninjas { get; set; }
        public virtual IEnumerable<Skill> Skills { get; set; }
    }
}
