using ManjDb.DataModels;
using ManjDb.TcLib;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace ManjDb
{
    public class Startup
    {
        #region Properties
        public IConfiguration Configuration { get; }

        public IHostEnvironment Environment { get; }
        #endregion

        public Startup(IConfiguration configuration, IHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Register DbContext
            services.AddDbContext<MyDbContext>();

            // Configure TcApiOptions
            services.Configure<TcApiOptions>(Configuration.GetSection($"{Environment.EnvironmentName}:TcApi"));

            // Register TcApi class
            _ = services.AddSingleton<TcApi>(provider =>
            {
                var httpClient = new HttpClient();
                var config = provider.GetRequiredService<IConfiguration>();
                var baseUrl = config["ApiBaseUrl"];
                var tcApiOptions = provider.GetRequiredService<IOptions<TcApiOptions>>();

                if (baseUrl == null)
                {
                    throw new ArgumentNullException(nameof(baseUrl), "BaseUrl cannot be null");
                }
                if (tcApiOptions?.Value == null)
                {
                    throw new ArgumentNullException(nameof(tcApiOptions), "TcApiOptions cannot be null");
                }

                return new TcApi(httpClient, baseUrl, tcApiOptions);
            });
        }
    }
}
