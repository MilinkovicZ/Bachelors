using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.IdentityModel.Logging;
using WebShop.Enums;
using WebShop.Models;

namespace WebShop.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> modelBuilder)
        {
            modelBuilder.HasKey(x => x.Id);
            modelBuilder.Property(x => x.Username).HasMaxLength(20).IsRequired();
            modelBuilder.HasIndex(x => x.Username).IsUnique();
            modelBuilder.Property(x => x.Email).HasMaxLength(30).IsRequired();
            modelBuilder.HasIndex(x => x.Email).IsUnique();
            modelBuilder.Property(x => x.FullName).HasMaxLength(30).IsRequired();
            modelBuilder.Property(x => x.Password).HasMaxLength(300).IsRequired();
            modelBuilder.Property(x => x.Address).HasMaxLength(40).IsRequired();
            modelBuilder.Property(x => x.UserType).HasConversion(new EnumToStringConverter<UserType>()).IsRequired(); //Cuva u BP string umesto broja
            modelBuilder.Property(x => x.VerificationState).HasConversion(new EnumToStringConverter<VerificationState>()).IsRequired();
            modelBuilder.Property(x => x.BirthDate).IsRequired();
            modelBuilder.Property(x => x.RegistrationTime).IsRequired();
            modelBuilder.Property(x => x.HasFullAccess).IsRequired().HasDefaultValue(false);

            modelBuilder.HasData(new User
            {
                Id = 1,
                Username = "zdravkoAdmin",
                Email = "zdravkoAdmin@gmail.com",
                FullName = "Zdravko Milinkovic",
                Password = BCrypt.Net.BCrypt.HashPassword("zdravkoAdmin"),
                Address = "Bihacka 33",
                UserType = UserType.Admin,
                VerificationState = VerificationState.Accepted,
                BirthDate = new DateTime(200, 8, 11),
                RegistrationTime = DateTime.Now,
                HasFullAccess = true
            });
        }
    }
}