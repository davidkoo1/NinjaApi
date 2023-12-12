using Microsoft.EntityFrameworkCore;
using NinjaWikiAPI.Data;
using NinjaWikiAPI.Interface;
using NinjaWikiAPI.Models;

namespace NinjaWikiAPI.Repository
{
    public class RankRepository : IRankRepository
    {
        private readonly DataContext _dataContext;

        public RankRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool Delete(Rank rank)
        {
           _dataContext.Remove(rank);
            return Save();
        }

        public async Task<Rank> GetRankById(int id) => await _dataContext.Ranks.FirstOrDefaultAsync(r => r.Id == id);

        public async Task<IEnumerable<Rank>> GetRanks() => await _dataContext.Ranks.ToListAsync();

        public bool Insert(Rank rank)
        {
            _dataContext.Add(rank);
            return Save();
        }

        public bool RankExists(int id) => _dataContext.Ranks.Any(r => r.Id == id);

        public bool Save() => _dataContext.SaveChanges() > 0 ? true : false;

        public bool Update(Rank rank)
        {
            _dataContext.Update(rank);
            return Save();
        }
    }
}
