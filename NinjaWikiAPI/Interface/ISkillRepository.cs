using NinjaWikiAPI.Models;

namespace NinjaWikiAPI.Interface
{
    public interface ISkillRepository
    {
        //GetSkills by category
        //GetSkills by rank
        //GetCategory by skill ??
        //GetRank by skill

        //SkillNinja Post
        //Get ninjas by skill
        //Get skills by ninja
        //GetSkill(TotalNinja)
        //ButtleTotalNinja, GroupByClan
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
