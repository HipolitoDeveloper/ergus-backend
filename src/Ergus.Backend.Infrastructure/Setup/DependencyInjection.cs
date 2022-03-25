﻿using Ergus.Backend.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ergus.Backend.Infrastructure.Setup
{
    public static class DependencyInjectionRepository
    {
        public static void AddDependencyInjectionRepositoryServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppClientContext>(o => { o.UseNpgsql(configuration.GetConnectionString("DefaultClient")); }, ServiceLifetime.Singleton);
            services.AddDbContext<AppServerContext>(o => { o.UseNpgsql(configuration.GetConnectionString("DefaultServer")); }, ServiceLifetime.Singleton);

            services.AddSingleton<IAdvertisementRepository, AdvertisementRepository>();
            services.AddSingleton<ICategoryRepository, CategoryRepository>();
            services.AddSingleton<IIntegrationRepository, IntegrationRepository>();
            services.AddSingleton<IMetadataRepository, MetadataRepository>();
            services.AddSingleton<IProducerRepository, ProducerRepository>();
            services.AddSingleton<IProductRepository, ProductRepository>();
            services.AddSingleton<IProductAttributeRepository, ProductAttributeRepository>();
            services.AddSingleton<IProviderRepository, ProviderRepository>();
            services.AddSingleton<ISkuRepository, SkuRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();
        }
    }
}
