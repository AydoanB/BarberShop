using Appointments.Data;
using Appointments.Data.Micro.Data;
using Appointments.Services;

namespace Appointments
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            ConfigureServices(builder.Services, builder.Configuration);

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            //app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            /*services.AddDbContext<ApplicationDbContext>(
               options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));*/
            /*services.Configure<CookiePolicyOptions>(
                options =>
                {
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                });*/

            services.Configure<MongoDBSettings>(configuration.GetSection("MongoDb"));

            services.AddScoped(typeof(MongoDbContext<>));

            services.AddControllers().AddJsonOptions(opt => opt.JsonSerializerOptions.PropertyNamingPolicy = null);

            services.AddRouting(opt => opt.LowercaseUrls = true);

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen();

            services.AddSingleton(configuration);

            // Application services
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IClientService, ClientService>();
        }
    }
}