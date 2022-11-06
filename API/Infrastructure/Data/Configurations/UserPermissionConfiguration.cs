using API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Infrastructure.Data.Configurations
{
    public class UserPermissionConfiguration : IEntityTypeConfiguration<UserPermission>
    {
        public void Configure(EntityTypeBuilder<UserPermission> entity)
        {
            entity.ToTable("UserPermissions");

            entity.HasOne(x => x.User)
                .WithMany(u => u.UserPermissions)
                .HasForeignKey(x => x.UserId);

            entity.HasOne(x => x.Permission)
                .WithMany(u => u.UserPermissions)
                .HasForeignKey(x => x.PermissionId);
        }
    }
}
