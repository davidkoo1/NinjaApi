using NinjaWikiAPI.Models;
using System.Xml.Linq;

namespace NinjaWikiAPI.Interface
{
    public interface IClanRepository
    {
        Task<IEnumerable<Clan>> GetClans();
        Task<Clan> GetClanById(int id);

        bool ClanExists(int id);

        bool Insert(Clan clan);
        bool Update(Clan clan);
        bool Delete(Clan clan);
        bool Save();
    }
}
