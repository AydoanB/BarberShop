using BarberShop.Identity.Data;
using BarberShop.Identity.Infrastructure;
using BarberShop.Identity.Services;
using BarberShop.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Identity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services
                .AddWebService<IdentityDbContext>(builder.Configuration)
                .AddUserStorage()
                .AddRouting(opt => opt.LowercaseUrls = true)
                .AddTransient<IIdentityService, IdentityService>()
                .AddTransient<ITokenGeneratorService, TokenGeneratorService>()
                .AddMessaging(builder.Configuration);

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app
                .UseWebService(builder.Environment);

            var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IdentityDbContext>();

            context.Database.Migrate();

            app.Run();
        }
    }
}