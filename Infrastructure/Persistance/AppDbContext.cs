using Domain;
using Infrastructure.Persistance.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance;

public class AppDbContext:DbContext
{
    public DbSet<Appointment> Appointments =>Set<Appointment>();
    public DbSet<Service> Services =>Set<Service>();
    public  DbSet<User> Users =>Set<User>();
    public DbSet<RefreshToken> RefreshTokens =>Set<RefreshToken>();
    public DbSet<AppointmentServiceLink> AppointmentServiceLinks =>Set<AppointmentServiceLink>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppointmentConfiguration).Assembly);
    }

    public AppDbContext(DbContextOptions options):base(options)
    {
        
    }
}