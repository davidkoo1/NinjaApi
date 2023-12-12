using NinjaWikiAPI.Models;

namespace NinjaWikiAPI.Interface
{
    public interface IRankRepository
    {
        Task<IEnumerable<Rank>> GetRanks();
        Task<Rank> GetRankById(int id);

        Task<Rank> GetRankByNinja(int ninjaId);

        bool RankExists(int id);

        bool Insert(Rank rank);
        bool Update(Rank rank);
        bool Delete(Rank rank);
        bool Save();
    }
}
