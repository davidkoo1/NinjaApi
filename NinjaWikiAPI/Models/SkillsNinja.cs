using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace NinjaWikiAPI.Models
{
    [PrimaryKey("Skill", "Ninja")]
    public class SkillsNinja
    {
        [ForeignKey("Skill")]
        public int SkillId { get; set; }

        [ForeignKey("Ninja")]
        public int NinjaId { get; set; }

        public virtual Skill Skill { get; set; }
        public virtual Ninja Ninja { get; set; }

    }
}
