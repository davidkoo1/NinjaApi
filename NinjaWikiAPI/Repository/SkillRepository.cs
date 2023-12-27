using Microsoft.EntityFrameworkCore;
using NinjaWikiAPI.Data;
using NinjaWikiAPI.Interface;
using NinjaWikiAPI.Models;

namespace NinjaWikiAPI.Repository
{
    public class SkillRepository : ISkillRepository
    {
        private readonly DataContext _dataContext;

        public SkillRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool Delete(Skill skill)
        {
            _dataContext.Remove(skill);
            return Save();
        }

        public async Task<IEnumerable<Skill>> GetSkills() => await _dataContext.Skills.Include(r => r.Rank).Include(c => c.Category).ToListAsync();

        public async Task<Skill> GetSkillById(int id) => await _dataContext.Skills.Include(r => r.Rank).Include(c => c.Category).FirstOrDefaultAsync(x => x.Id == id);

        public async Task<IEnumerable<Ninja>> GetNinjasBySkill(int skillId) => await _dataContext.SkillsNinja
                             .Where(s => s.SkillId == skillId)
                             .Select(n => n.Ninja)
                             .ToListAsync();

        public bool Insert(Skill skill)
        {
            _dataContext.Add(skill);
            return Save();
        }

        public bool Save() => _dataContext.SaveChanges() > 0 ? true : false;

        public bool SkillExists(int id) => _dataContext.Skills.Any(x => x.Id == id);

        public bool Update(Skill skill)
        {
            _dataContext.Update(skill);
            return Save();
        }
    }
}
