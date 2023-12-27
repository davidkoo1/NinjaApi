using Microsoft.EntityFrameworkCore;
using NinjaWikiAPI.Data;
using NinjaWikiAPI.Interface;
using NinjaWikiAPI.Models;

namespace NinjaWikiAPI.Repository
{
    public class NinjaRepository : INinjaRepository
    {
        private readonly DataContext _dataContext;

        public NinjaRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool Delete(Ninja ninja)
        {
            _dataContext.Remove(ninja);
            return Save();
        }

        public async Task<Ninja> GetNinjaById(int id) => await _dataContext.Ninjas.Include(c => c.Clan).Include(r => r.Rank).Include(v => v.Village).FirstOrDefaultAsync(n => n.Id == id);

        public async Task<IEnumerable<Ninja>> GetNinjas(int limit, int offset) => await _dataContext.Ninjas
            .OrderBy(n => n.Id)
            .Include(c => c.Clan).Include(r => r.Rank).Include(v => v.Village)
            .Skip(offset).Take(limit).ToListAsync();    //ByRank
        public async Task<IEnumerable<Ninja>> GetNinjas() => await _dataContext.Ninjas.OrderBy(n => n.Id).ToListAsync();   

        public bool Insert(Ninja ninja)
        {
            _dataContext.Add(ninja);
            return Save();
        }

        public bool NinjaExists(int id) => _dataContext.Ninjas.Any(x => x.Id == id);

        public bool Save() => _dataContext.SaveChanges() > 0 ? true : false;

        public bool Update(Ninja ninja)
        {
            _dataContext.Update(ninja);
            return Save();
        }
    }
}
