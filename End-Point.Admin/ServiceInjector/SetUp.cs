using Services.Interfaces;
using Services.Services;

namespace End_Point.Admin.ServiceInjector
{
    public static class SetUp
    {
        public static IServiceCollection InjectServices(this IServiceCollection services,IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddTransient<IPersonService>(provider =>
            {
                return new PersonService(connectionString);
            });
            return services;
        }
    }
}
