using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace NinjaWikiAPI.Models
{
    public class SkillsNinja
    {
        public int SkillId { get; set; }
        public int NinjaId { get; set; }

        public virtual Skill Skill { get; set; }
        public virtual Ninja Ninja { get; set; }

    }
}
