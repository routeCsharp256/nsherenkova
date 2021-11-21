
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OzonEdu.MerchandiseService.GrpcServices;
using OzonEdu.MerchandiseService.Infrastructure.Configuration;
using OzonEdu.MerchandiseService.Infrastructure.Interceptors;
using OzonEdu.MerchandiseService.Services;
using OzonEdu.MerchandiseService.Services.Interfaces;


namespace OzonEdu.MerchandiseService
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<DatabaseConnectionOptions>(Configuration.GetSection(nameof(DatabaseConnectionOptions)));
            services.AddSingleton<IMerchService, MerchService>();
            services.AddInfrastructureServices();
            services.AddDatabaseComponents();
            services.AddInfrastructureRepositories();
            services.AddGrpc(options => options.Interceptors.Add<LoggingInterceptor>());

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<MerchGrpcService>();
                endpoints.MapControllers();
            });

        }
    }
}