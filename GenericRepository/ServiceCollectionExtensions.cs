using GenericRepository.Repositories;
using GenericRepository.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GenericRepository
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Register all the necessary services to the ASP.NET Core Dependency Injection container.
        /// </summary>
        /// <typeparam name="TDbContext">Entity Framework Core DbContext.</typeparam>
        /// <param name="services">The type to be extended.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="services"/> is NULL.</exception>
        /// <exception cref="ApplicationException">Thrown if <typeparamref name="TDbContext"/> is NULL.</exception>
        public static void AddGenericRepository<TDbContext>(this IServiceCollection services)
            where TDbContext : DbContext
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            DbContext dbContext = serviceProvider.GetService<TDbContext>();

            if (dbContext == null)
            {
                throw new ApplicationException($"Please register your {typeof(TDbContext)} before calling {nameof(AddGenericRepository)}.");
            }

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>(uow => new UnitOfWork(dbContext));

        }
    }
}
