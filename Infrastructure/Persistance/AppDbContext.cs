using Domain;
using Infrastructure.Persistance.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance;

public class AppDbContext:DbContext
{
    public DbSet<Appointment> Appointments =>Set<Appointment>();
    public DbSet<Service> Services =>Set<Service>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppointmentConfiguration).Assembly);
    }

    public AppDbContext(DbContextOptions options):base(options)
    {
        
    }
}