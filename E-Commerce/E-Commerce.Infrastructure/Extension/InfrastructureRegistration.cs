using E_Commerce.Core.Interfaces.External;
using E_Commerce.Core.Interfaces.Repositories;
using E_Commerce.Core.Interfaces.Repositories.Base;
using E_Commerce.Infrastructure.External.Mailing;
using E_Commerce.Infrastructure.External.Payments;
using E_Commerce.Infrastructure.Persistence.Repositories;
using E_Commerce.Infrastructure.Persistence.Repositories.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace E_Commerce.Infrastructure.Extension
{
    public static class InfrastructureRegistration
    {
        public static void AddInfrastructureRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            // Register repositories
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICouponRepository, CouponRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();

            // Register external services
            services.AddScoped<IStripePayment, StripePayment>();
            services.AddScoped<IEmailSender, EmailSenderServices>();
        }
    }
}  
