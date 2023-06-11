using BarberShop.Appointments.Data;
using BarberShop.Appointments.Data.Micro.Data;
using BarberShop.Appointments.Messages;
using BarberShop.Appointments.Services;
using BarberShop.Infrastructure;
using BarberShop.Services;

namespace BarberShop.Appointments
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ConfigureServices(builder.Services, builder.Configuration);

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app
                .UseWebService(builder.Environment);

            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDBSettings>(configuration.GetSection(nameof(MongoDBSettings)));

            services.AddScoped(typeof(MongoDbContext<>));

            services.AddControllers();

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen();

            services.AddWebService(configuration);

            services.AddEndpointsApiExplorer();

            services.AddSingleton(configuration);

            services
                .AddScoped<IAppointmentService, AppointmentService>()
                .AddScoped<IClientService, ClientService>()
                .AddScoped<IBarberService, BarberService>()
                .AddScoped<ICurrentTokenService, CurrentTokenService>()
                .AddMessaging(configuration, typeof(UserBarberCreatedConsumer));
        }
    }
}