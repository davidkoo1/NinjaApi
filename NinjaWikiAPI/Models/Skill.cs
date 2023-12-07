using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NinjaWikiAPI.Models
{
    public class Skill
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }


        [ForeignKey("Category")]
        public int CategoryId { get; set; }


        [ForeignKey("Rank")]
        public int RankId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Rank Rank { get; set; }

        public virtual IList<SkillsNinja> SkillsNinjas { get; set; }
    }
}
