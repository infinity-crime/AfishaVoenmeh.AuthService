using AfishaVoenmeh.AuthService.Domain.UserAggregate.Entities;
using AfishaVoenmeh.AuthService.Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AfishaVoenmeh.AuthService.Infrastructure.Data.Configurations;

public class RefreshSessionConfiguration : IEntityTypeConfiguration<RefreshSession>
{
    public void Configure(EntityTypeBuilder<RefreshSession> builder)
    {
        builder.ToTable("RefreshSessions");

        builder.HasKey(rs => rs.Id);

        builder.Property(rs => rs.Id)
            .HasConversion(
                id => id.Value,
                value => RefreshSessionId.Create(value))
            .IsRequired();

        builder.Property(rs => rs.UserId)
            .HasConversion(
                userId => userId.Value,
                value => UserId.CreateFrom(value).Value)
            .IsRequired();

        builder.Property(rs => rs.Token)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(rs => rs.ExpiresAt)
            .IsRequired()
            .HasColumnType("timestamp with time zone");

        builder.Property(rs => rs.CreatedAt)
            .IsRequired()
            .HasColumnType("timestamp with time zone");

        builder.HasIndex(rs => rs.Token)
            .HasDatabaseName("IX_RefreshSessions_Token")
            .IsUnique();
    }
}
