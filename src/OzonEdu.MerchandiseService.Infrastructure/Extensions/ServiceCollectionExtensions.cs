using System;
using MediatR;
using OzonEdu.MerchandiseService.Infrastructure.DomainServices;
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
        
        /// <summary>
        /// Добавление в DI контейнер инфраструктурных репозиториев
        /// </summary>
        /// <param name="services">Объект IServiceCollection</param>
        /// <returns>Объект <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddInfrastructureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IMerchandiseRequestRepository, MerchandiseRequestRepository>();
            services.AddScoped<IManagerRepository, ManagerRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            return services;
        }
    }
}