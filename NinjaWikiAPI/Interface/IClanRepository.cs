using NinjaWikiAPI.Models;

namespace NinjaWikiAPI.Interface
{
    public interface IClanRepository
    {
        Task<IList<Clan>> GetClans();
    }
}
