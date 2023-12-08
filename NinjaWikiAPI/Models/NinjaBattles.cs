using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace NinjaWikiAPI.Models
{
    public class NinjaBattles
    {
        public int NinjaId { get; set; }
        public int BattleId { get; set; }

        public virtual Ninja Ninja { get; set; }
        public virtual Battle Battle { get; set; }
    }
}
