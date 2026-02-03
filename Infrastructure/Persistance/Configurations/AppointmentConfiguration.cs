using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations;

public class AppointmentConfiguration:IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.HasKey(e => e.AppointmentId);

        builder.Property(x => x.AppointmentId)
            .IsRequired();
            
        builder.Property(x => x.ServiceId)
            .IsRequired();
            
        builder.Property(x => x.StartTime)
            .IsRequired();
            
        builder.Property(x => x.EndTime)
            .IsRequired();
            
        builder.Property(x=>x.Title)
            .IsRequired()
            .HasMaxLength(20);;
    }
}