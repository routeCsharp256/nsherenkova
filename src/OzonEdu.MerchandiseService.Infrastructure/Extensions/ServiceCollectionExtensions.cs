using MediatR;
using Npgsql;
using OzonEdu.MerchandiseService.Infrastructure.DomainServices.Implementation;
using OzonEdu.MerchandiseService.Infrastructure.DomainServices.Infrastructure;
using OzonEdu.MerchandiseService.Infrastructure.DomainServices.Infrastructure.Interfaces;
using OzonEdu.MerchandiseService.Infrastructure.DomainServices.Interfaces;
using OzonEdu.MerchandiseService.Infrastructure.Handlers;
using OzonEdu.MerchandiseService.Infrastukture.Contracts;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Класс расширений для типа <see cref="IServiceCollection"/> для регистрации инфраструктурных сервисов
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Добавление в DI контейнер инфраструктурных сервисов
        /// </summary>
        /// <param name="services">Объект IServiceCollection</param>
        /// <returns>Объект <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddMediatR(typeof(CreateMerchandiseRequestCommandHandler).Assembly);
            
            return services;
        }
        public static IServiceCollection AddDatabaseComponents(this IServiceCollection services)
        {
            services.AddScoped<IDbConnectionFactory<NpgsqlConnection>, NpgsqlConnectionFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IChangeTracker, ChangeTracker>();
            return services;
        }
        /// <summary>
        /// Добавление в DI контейнер инфраструктурных репозиториев
        /// </summary>
        /// <param name="services">Объект IServiceCollection</param>
        /// <returns>Объект <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddInfrastructureRepositories(this IServiceCollection services)
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
            services.AddScoped<IMerchandiseRequestRepository, MerchandiseRequestRepository>();
            services.AddScoped<IManagerRepository, ManagerRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            return services;
        }
    }
}