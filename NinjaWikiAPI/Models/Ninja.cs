using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NinjaWikiAPI.Models
{
    public class Ninja
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsTraitor { get; set; }


        public virtual Clan Clan { get; set; }
        public virtual Village Village { get; set; }
        public virtual Rank Rank { get; set; }

        public virtual IList<NinjaBattles> NinjaBattles { get; set; }
        public virtual IList<SkillsNinja> SkillsNinja { get; set;}
    }
}
