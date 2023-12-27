using Microsoft.EntityFrameworkCore;
using NinjaWikiAPI.Data;
using NinjaWikiAPI.Interface;
using NinjaWikiAPI.Models;

namespace NinjaWikiAPI.Repository
{
    public class VillageRepository : IVillageRepository
    {
        private readonly DataContext _dataContext;

        public VillageRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool Delete(Village village)
        {
            _dataContext.Remove(village);
            return Save();
        }

        public async Task<Village> GetVillageById(int id) => await _dataContext.Villages.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Village> GetVillageByNinja(int ninjaId) => await _dataContext.Ninjas.Where(n => n.Id == ninjaId).Select(n => n.Village).FirstOrDefaultAsync();

        public async Task<IEnumerable<Village>> GetVillages() => await _dataContext.Villages.ToListAsync();

        public bool Insert(Village village)
        {
            _dataContext.Add(village);
            return Save();
        }

        public bool Save() => _dataContext.SaveChanges() > 0 ? true : false;

        public bool Update(Village village)
        {
            _dataContext.Update(village);
            return Save();
        }

        public bool VillageExists(int id) => _dataContext.Villages.Any(x => x.Id == id);
    }
}
