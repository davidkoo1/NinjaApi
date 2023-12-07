using System.ComponentModel.DataAnnotations;

namespace NinjaWikiAPI.Models
{
    public class Rank
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }

        public virtual IList<Ninja> Ninjas { get; set; }
        public virtual IList<Skill> Skills { get; set; }
    }
}
