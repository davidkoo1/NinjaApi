using Microsoft.EntityFrameworkCore;
using NinjaWikiAPI.Data;
using NinjaWikiAPI.Interface;
using NinjaWikiAPI.Models;

namespace NinjaWikiAPI.Repository
{
    public class ClanRepository : IClanRepository
    {
        private readonly DataContext _dataContext;

        public ClanRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool ClanExists(int id) => _dataContext.Clans.Any(c => c.Id == id);

        public bool Delete(Clan clan)
        {
            _dataContext.Remove(clan);
            return Save();
        }

        public async Task<Clan> GetClanById(int id) => await _dataContext.Clans.Include(c => c.Ninjas).FirstOrDefaultAsync(c => c.Id == id);

        public async Task<IEnumerable<Clan>> GetClans() => await _dataContext.Clans.OrderBy(c => c.Name).Include(c => c.Ninjas).ToListAsync();

        public bool Insert(Clan clan)
        {
            _dataContext.Add(clan);
            return Save();
        }

        public bool Save() => _dataContext.SaveChanges() > 0 ? true : false;

        public bool Update(Clan clan)
        {
            _dataContext.Update(clan);
            return Save();
        }
    }
}
