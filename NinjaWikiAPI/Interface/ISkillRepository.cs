using NinjaWikiAPI.Models;

namespace NinjaWikiAPI.Interface
{
    public interface ISkillRepository
    {
        Task<IEnumerable<Skill>> GetSkills();
        Task<Skill> GetSkillById(int id);

        //Task<Ninja> GetCategoryBySkill(int skillId);
        Task<IEnumerable<Ninja>> GetNinjasBySkill(int skillId);

        bool SkillExists(int id);

        bool Insert(Skill skill);
        bool Update(Skill skill);
        bool Delete(Skill skill);
        bool Save();
    }
}
