using BarberShop.Identity.Data;
using BarberShop.Identity.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace BarberShop.Identity.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUserStorage(
        this IServiceCollection services)
    {
        services
            .AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<IdentityDbContext>();

        return services;
    }
}