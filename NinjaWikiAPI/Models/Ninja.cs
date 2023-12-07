using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NinjaWikiAPI.Models
{
    public class Ninja
    {
        [Key, Column(Order = 0)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsTraitor { get; set; }

        [ForeignKey("Clan")]
        public int ClanId { get; set; }

        [ForeignKey("Village")]
        public int VillageId { get; set; }

        [ForeignKey("Rank")]
        public int RankId { get; set; }

        public virtual Clan Clan { get; set; }
        public virtual Village Village { get; set; }
        public virtual Rank Rank { get; set; }

        public virtual IList<NinjaBattles> NinjaBattles { get; set; }
        public virtual IList<SkillsNinja> SkillsNinjas { get; set;}
    }
}
