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
        public async Task<IList<Clan>> GetClans() => await _dataContext.Clans.OrderBy(c => c.Name).ToListAsync();
    }
}
