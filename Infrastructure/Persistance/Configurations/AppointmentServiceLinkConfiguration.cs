using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations;

public class AppointmentServiceLinkConfiguration:IEntityTypeConfiguration<AppointmentServiceLink>
{
    public void Configure(EntityTypeBuilder<AppointmentServiceLink> builder)
    {
        builder.ToTable("AppointmentServiceLinks");
        
        builder.HasKey(x => new {x.AppointmentId, x.ServiceId});
        
        builder.HasOne(x=>x.Appointment)
            .WithMany(x=>x.AppointmentServices)
            .HasForeignKey(x=>x.AppointmentId);
        
        builder.HasOne(x => x.Service)
            .WithMany(x=>x.AppointmentServices)
            .HasForeignKey(x=>x.ServiceId);
    }
}