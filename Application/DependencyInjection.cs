using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;
namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAppointmentService,AppointmentService>();
        services.AddScoped<IServiceAppService,ServiceAppService>();
        services.AddScoped<IUserService,UserService>();
        
        return services;
    }
}