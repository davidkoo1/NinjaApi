using NinjaWikiAPI.Interface;
using NinjaWikiAPI.Repository;

namespace NinjaWikiAPI.Helper
{
    public class RepositoriesModels
    {
        public RepositoriesModels(IServiceCollection services)
        {
            services.AddTransient<IClanRepository, ClanRepository>();
            services.AddTransient<INinjaRepository, NinjaRepository>();
            services.AddTransient<IRankRepository, RankRepository>();
            services.AddTransient<IVillageRepository, VillageRepository>();
        }
    }
}
