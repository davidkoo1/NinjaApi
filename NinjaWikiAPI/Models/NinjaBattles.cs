using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace NinjaWikiAPI.Models
{
    [PrimaryKey("Ninja", "Battle")]
    public class NinjaBattles
    {
        [ForeignKey("Ninja")]
        public int NinjaId { get; set; }

        [ForeignKey("Battle")]
        public int BattleId { get; set; }

        public virtual Ninja Ninja { get; set; }
        public virtual Battle Battle { get; set; }
    }
}
