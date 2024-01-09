using Microsoft.Extensions.DependencyInjection;

namespace http_pratice.DAL
{
    public static class ServiceExtensionsDal
    {
        public static void AddStudentServicesDal(this IServiceCollection services)
        {
            services.AddSingleton<IStudentDal, StudentDal>();
        }
    }
}
