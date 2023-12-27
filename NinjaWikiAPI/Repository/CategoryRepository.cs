using Microsoft.EntityFrameworkCore;
using NinjaWikiAPI.Data;
using NinjaWikiAPI.Interface;
using NinjaWikiAPI.Models;

namespace NinjaWikiAPI.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _dataContext;

        public CategoryRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool CategoryExists(int id) => _dataContext.Categories.Any(c => c.Id == id);

        public bool Delete(Category category)
        {
            _dataContext.Remove(category);
            return Save();
        }

        public async Task<IEnumerable<Category>> GetCategories() => await _dataContext.Categories.ToListAsync();

        public async Task<Category> GetCategoryBySkill(int skillId) => await _dataContext.Skills.Where(s => s.Id == skillId).Select(s => s.Category).FirstOrDefaultAsync();

        public async Task<Category> GetCategoryById(int id) => await _dataContext.Categories.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<IEnumerable<Skill>> GetSkillsByCategory(int categoryId) => await _dataContext.Categories.Where(c => c.Id == categoryId).SelectMany(c => c.Skills).ToListAsync();

        public bool Insert(Category category)
        {
            _dataContext.Add(category);
            return Save();
        }

        public bool Save() => _dataContext.SaveChanges() > 0 ? true : false;

        public bool Update(Category category)
        {
            _dataContext.Update(category);
            return Save();
        }
    }
}
