using NinjaWikiAPI.Models;

namespace NinjaWikiAPI.Interface
{
    public interface INinjaRepository
    {
        Task<IEnumerable<Ninja>> GetNinjas(int limit, int offset);
        Task<IEnumerable<Ninja>> GetNinjas();
        Task<Ninja> GetNinjaById(int id);

        bool NinjaExists(int id);

        bool Insert(Ninja ninja);
        bool Update(Ninja ninja);
        bool Delete(Ninja ninja);
        bool Save();
    }
}
