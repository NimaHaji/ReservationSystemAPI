using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance;

public class AppDbContext:DbContext
{
    public DbSet<Appointment> Appointments =>Set<Appointment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(x => x.Id)
                .IsRequired();
            
            entity.Property(x => x.ServiceId)
                .IsRequired();
            
            entity.Property(x => x.StartTime)
                .IsRequired();
            
            entity.Property(x => x.EndTime)
                .IsRequired();
            
            entity.Property(x=>x.Title)
                .IsRequired()
                .HasMaxLength(20);;
        });
    }

    public AppDbContext(DbContextOptions options):base(options)
    {
        
    }
}