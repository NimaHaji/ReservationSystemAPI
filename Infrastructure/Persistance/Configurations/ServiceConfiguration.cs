using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations;

public class ServiceConfiguration:IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.ToTable("Service");
        
        builder.HasKey(s => s.Id);
        
        builder.Property(s => s.Title)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(s => s.DurationMinutes);

        builder.Property(x => x.Title)
            .IsRequired();
        
        builder.HasMany(x=>x.AppointmentServices)
            .WithOne(x=>x.Service)
            .HasForeignKey(x=>x.ServiceId);
    }
}