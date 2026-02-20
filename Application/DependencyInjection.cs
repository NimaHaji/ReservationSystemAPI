using Application.Features.Appointments.Interfaces;
using Application.Features.Appointments.Services;
using Application.Features.Auth.Interfaces;
using Application.Features.Auth.Services;
using Application.Features.Service.Services;
using Microsoft.Extensions.DependencyInjection;
namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAppointmentService,AppointmentService>();
        services.AddScoped<IServiceAppService,ServiceAppService>();
        services.AddScoped<IUserService,UserService>();
        services.AddScoped<IPasswordRecoveryService,PasswordRecoveryService>();
        return services;
    }
}