using Microsoft.Extensions.DependencyInjection;

namespace http_pratice.Domain
{
    public static class ServiceExtensionsDomain
    {
        public static void AddStudentServices(this IServiceCollection services)
        {
            services.AddSingleton<IStudentDomain, StudentDomain>();
        }
    }
}