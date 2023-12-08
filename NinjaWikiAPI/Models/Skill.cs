using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NinjaWikiAPI.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public virtual Category Category { get; set; }
        public virtual Rank Rank { get; set; }

        public virtual IList<SkillsNinja> SkillsNinja { get; set; }
    }
}
