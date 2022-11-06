using API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Infrastructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.ToTable("Users");

            entity.Property(x => x.FirstName).HasMaxLength(250);

            entity.Property(x => x.LastName).HasMaxLength(250);

            entity.Property(x => x.Username).HasMaxLength(250);

            entity.Property(x => x.Email).HasMaxLength(250);

            entity.Property(x => x.Status).HasMaxLength(250);

            entity.Property(x => x.Password).HasMaxLength(250);
        }
    }
}
