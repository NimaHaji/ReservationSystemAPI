using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Application.Features.Appointments.Interfaces;
using Application.Features.AppointmentServiceLink.Interfaces;
using Application.Features.Auth.Interfaces;
using Application.Features.Service.Interfaces;
using Infrastructure.Persistance;
using Infrastructure.Persistance.Repositories;
using Infrastructure.Security.Context;
using Infrastructure.Security.Hashing;
using Infrastructure.Security.Jwt;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddHttpContextAccessor();
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        services.AddScoped<IServiceRepository, ServiceRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPasswordHasher, PasswordHasherService>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IAppointmenServiceLinkRepository, AppointmentServiceLinkRepository>();
        services.AddScoped<IUSerContext, UserContext>();
        
        return services;
    }
}