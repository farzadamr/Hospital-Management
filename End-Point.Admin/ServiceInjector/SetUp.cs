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
            services.AddTransient<IDoctorService>(provider =>
            {
                return new DoctorService(connectionString);
            });
            services.AddTransient<IDepartmentService>(provider =>
            {
                return new DepartmentService(connectionString);
            });
            services.AddTransient<IPatientService>(provider =>
            {
                return new PatientService(connectionString);
            });
            return services;
        }
    }
}
