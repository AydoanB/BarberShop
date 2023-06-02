using BarberShop.Appointments.Data;
using BarberShop.Appointments.Data.Micro.Data;
using BarberShop.Appointments.Services;
using BarberShop.Infrastructure;

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
                .UseHttpsRedirection()
                .UseRouting()
                .UseCors(options => options
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod())
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints => endpoints
                    .MapControllers());

            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDBSettings>(configuration.GetSection(nameof(MongoDBSettings)));

            services.AddScoped(typeof(MongoDbContext<>));

            services.AddControllers().AddJsonOptions(opt => opt.JsonSerializerOptions.PropertyNamingPolicy = null);

            services.AddRouting(opt => opt.LowercaseUrls = true);

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen();

            services.AddWebService(configuration);

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen();

            services.AddSingleton(configuration);

            services
                .AddScoped<IAppointmentService, AppointmentService>()
                .AddScoped<IClientService, ClientService>()
                .AddScoped<IBarberService, BarberService>();
        }
    }
}