using API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
