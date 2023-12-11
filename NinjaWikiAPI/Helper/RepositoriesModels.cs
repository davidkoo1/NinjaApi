using NinjaWikiAPI.Interface;
using NinjaWikiAPI.Repository;

namespace NinjaWikiAPI.Helper
{
    public class RepositoriesModels
    {
        public RepositoriesModels(IServiceCollection services)
        {
            services.AddTransient<IClanRepository, ClanRepository>();
        }
    }
}
