using Microsoft.EntityFrameworkCore;
using NinjaWikiAPI.Models;

namespace NinjaWikiAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Battle> Battles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Clan> Clans { get; set; }
        public DbSet<Ninja> Ninjas { get; set; }
        public DbSet<Rank> Ranks { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Village> Villages { get; set; }
        public DbSet<NinjaBattles> NinjaBattles { get; set; }
        
        public DbSet<SkillsNinja> SkillsNinjas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Ignore("NinjaBattles");
            base.OnModelCreating(modelBuilder);
            /*
            modelBuilder.Entity<NinjaBattles>().HasKey(nb => new {nb.NinjaId, nb.BattleId});
            modelBuilder.Entity<NinjaBattles>().HasOne(n => n.Ninja).WithMany(nb => nb.NinjaBattles).HasForeignKey(b => b.BattleId);
            modelBuilder.Entity<NinjaBattles>().HasOne(b => b.Battle).WithMany(nb => nb.NinjaBattles).HasForeignKey(n => n.NinjaId);


            modelBuilder.Entity<SkillsNinja>().HasKey(ns => new { ns.SkillId, ns.NinjaId });
            modelBuilder.Entity<SkillsNinja>().HasOne(s => s.Skill).WithMany(ns => ns.SkillsNinjas).HasForeignKey(n => n.NinjaId);
            modelBuilder.Entity<SkillsNinja>().HasOne(n => n.Ninja).WithMany(ns => ns.SkillsNinjas).HasForeignKey(s => s.SkillId);
            */
            
        }

    }
}
