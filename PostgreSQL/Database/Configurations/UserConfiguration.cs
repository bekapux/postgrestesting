using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostgreSQL.Domain;
using System.Text.Json;

namespace PostgreSQL.Database.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.MiddleName)
            .HasMaxLength(100);

        builder.Property(u => u.DateOfBirth)
            .IsRequired();

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(u => u.PhoneNumber)
            .HasMaxLength(100);

        builder.Property(u => u.Gender)
            .HasMaxLength(100);

        builder.Property(u => u.Username)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.PasswordHash)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(u => u.CreatedAt)
            .IsRequired();

        builder.Property(u => u.UpdatedAt)
            .IsRequired();

        builder.Property(u => u.IsActive)
            .IsRequired();

        builder.Property(u => u.ProfilePictureUrl)
            .HasMaxLength(500);

        builder.Property(u => u.Bio)
            .HasMaxLength(1000);

        builder.Property(u => u.Roles)
            .HasConversion(
                v => JsonSerializer.Serialize(v, default(JsonSerializerOptions)),
                v => JsonSerializer.Deserialize<List<string>>(v, default(JsonSerializerOptions))!)
            .HasColumnType("json");

        builder.Property(u => u.Occupation)
            .HasMaxLength(100);

        builder.Property(u => u.CompanyName)
            .HasMaxLength(200);

        builder.Property(u => u.Nationality)
            .HasMaxLength(100);

        builder.Property(u => u.MaritalStatus)
            .HasMaxLength(50);

        builder.Property(u => u.FriendIds)
            .HasConversion(
                v => JsonSerializer.Serialize(v, default(JsonSerializerOptions)),
                v => JsonSerializer.Deserialize<List<Guid>>(v, default(JsonSerializerOptions))!)
            .HasColumnType("json");

        builder.Property(u => u.SocialSecurityNumber)
            .HasMaxLength(100);

        builder.Property(u => u.PassportNumber)
            .HasMaxLength(100);

        builder.Property(u => u.Interests)
            .HasConversion(
                v => JsonSerializer.Serialize(v, default(JsonSerializerOptions)),
                v => JsonSerializer.Deserialize<List<string>>(v, default(JsonSerializerOptions))!)
            .HasColumnType("json");

        builder.Property(u => u.LastLoginAt);

        builder.Property(u => u.Address)
            .HasConversion(
                v => JsonSerializer.Serialize(v, default(JsonSerializerOptions)),
                v => JsonSerializer.Deserialize<Address>(v, default(JsonSerializerOptions)))
            .HasColumnType("json");
    }
}
