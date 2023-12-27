using NinjaWikiAPI.Models;

namespace NinjaWikiAPI.Interface
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> GetCategoryById(int id);

        Task<Category> GetCategoryBySkill(int skillId);
        Task<IEnumerable<Skill>> GetSkillsByCategory(int categoryId);

        bool CategoryExists(int id);

        bool Insert(Category category);
        bool Update(Category category);
        bool Delete(Category category);
        bool Save();
    }
}
