using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;
namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAppointmentService,AppointmentService>();
        services.AddScoped<IServiceAppService,ServiceAppService>();
        
        return services;
    }
}