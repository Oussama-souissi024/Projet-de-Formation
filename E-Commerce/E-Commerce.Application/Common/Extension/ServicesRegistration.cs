using E_Commerce.Application.Auth.Interfaces;
using E_Commerce.Application.Auth.Services;
using E_Commerce.Application.Cart.Interfaces;
using E_Commerce.Application.Cart.Services;
using E_Commerce.Application.CategoryE.Interfaces;
using E_Commerce.Application.CategoryE.Services;
using E_Commerce.Application.Common.Helpers.Implementations;
using E_Commerce.Application.Common.Helpers.Interfaces;
using E_Commerce.Application.Coupons.Interfaces;
using E_Commerce.Application.Coupons.Services;
using E_Commerce.Application.Orders.Interfaces;
using E_Commerce.Application.Orders.Services;
using E_Commerce.Application.Payments.Services;
using E_Commerce.Application.Products.Interfaces;
using E_Commerce.Application.Products.Services;
using Microsoft.Extensions.DependencyInjection;
using E_Commerce.Core.Interfaces.External;
using E_Commerce.Infrastructure.External.Payments;
using E_Commerce.Application.Coupons.Mappings;
using E_Commerce.Application.Products.Mappings;
using E_Commerce.Application.CategoryE.Mappings;
using E_Commerce.Application.Cart.Mappings;
using E_Commerce.Application.Orders.Mappings;

namespace E_Commerce.Application.Common.Extension
{
    public static class ServicesRegistration
    {
        public static void AddServiceRegistration(this IServiceCollection services)
        {
            // Add AutoMapper
            services.AddAutoMapper(typeof(CouponProfile));
            services.AddAutoMapper(typeof(ProductProfile));
            services.AddAutoMapper(typeof(CategoryProfile));
            services.AddAutoMapper(typeof(CartProfile));
            services.AddAutoMapper(typeof(OrderProfile));

            // Add Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ICategoryServise, CategoryServise>();
            services.AddScoped<ICouponService, CouponService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IStripePayment, StripePayment>();

            // Add Helpers
            services.AddScoped<IFileHelper, FileHelper>();
        }
    }
}
