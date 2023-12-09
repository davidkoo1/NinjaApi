using NinjaWikiAPI.Interface;

namespace NinjaWikiAPI.Repository
{
    public class RepositoriesModels
    {
        public RepositoriesModels(IServiceCollection services)
        {
            services.AddTransient<IClanRepository, ClanRepository>();
        }
    }
}
