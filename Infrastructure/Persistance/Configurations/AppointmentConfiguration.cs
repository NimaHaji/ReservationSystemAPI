using Domain.Entities;
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
       
        builder.Property(x=>x.Title)
            .IsRequired()
            .HasMaxLength(20);;
            
        builder.Property(x => x.StartTime)
            .IsRequired();
            
        builder.Property(x => x.EndTime)
            .IsRequired();
            
        builder.Property(x=>x.Status)
            .IsRequired();
        
        builder.HasOne(x=>x.User)
            .WithMany(x=>x.Appointments)
            .HasForeignKey(x=>x.UserId);
        
        builder.HasMany(x=>x.AppointmentServices)
            .WithOne(x=>x.Appointment)
            .HasForeignKey(x=>x.AppointmentId);
        
        builder.HasCheckConstraint(
            "CK_Status_Valid_Values",
            "[Status] IN (0, 1,2)"
        );
    }
}