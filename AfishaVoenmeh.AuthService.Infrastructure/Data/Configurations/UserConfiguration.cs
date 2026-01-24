using AfishaVoenmeh.AuthService.Domain.UserAggregate;
using AfishaVoenmeh.AuthService.Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AfishaVoenmeh.AuthService.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .HasConversion(id => id.Value, value => UserId.CreateFrom(value).Value)
            .ValueGeneratedNever();

        builder.OwnsOne(u => u.Credentials, onb =>
        {
            onb.Property(c => c.FirstName).HasColumnName("FirstName");
            onb.Property(c => c.LastName).HasColumnName("LastName");
            onb.Property(c => c.Patronymic).HasColumnName("Patronymic");
        });

        builder.OwnsOne(u => u.Email, onb =>
        {
            onb.Property(e => e.Value).HasColumnName("Email");
        });

        builder.OwnsOne(u => u.PhoneNumber, onb =>
        {
            onb.Property(pn => pn.Value).HasColumnName("PhoneNumber");
        });

        builder.OwnsOne(u => u.PasswordHash, onb =>
        {
            onb.Property(ph => ph.Value).HasColumnName("PasswordHash");
        });
    }
}