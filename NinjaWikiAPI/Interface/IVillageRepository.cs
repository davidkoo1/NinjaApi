using NinjaWikiAPI.Models;

namespace NinjaWikiAPI.Interface
{
    public interface IVillageRepository
    {
        Task<IEnumerable<Village>> GetVillages();
        Task<Village> GetVillageById(int id);

        Task<Village> GetVillageByNinja(int ninjaId);

        bool VillageExists(int id);

        bool Insert(Village village);
        bool Update(Village village);
        bool Delete(Village village);
        bool Save();
    }
}
