using Microsoft.Extensions.DependencyInjection;
using Sportradar.DataProviders;
using Sportradar.DataProviders.Interfaces;
using Sportradar.Services;
using Sportradar.Services.Interfaces;

namespace Sportradar.IoC
{
    public class LibraryConfiguration
    {
        public void ConfigureServices(IServiceCollection services)
        {
            RegisterServices(services);
            RegisterDataProviders(services);
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IScoreboard, Scoreboard>();
        }

        private void RegisterDataProviders(IServiceCollection services)
        {
            services.AddSingleton<IFootballDataProvider, FootballDataProvider>();
        }
    }
}
