using API.Domain.Entities;
using API.Interfaces;
using API.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Persistence
{
    public class UserRepository : IUserRepository
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public UserRepository(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public void Add(User user)
        {
            _applicationDbContext.Users.Add(user);
        }

        public List<User> GetAllUsers()
        {
            return _applicationDbContext.Users.ToList();
        }

        public User? GetUserById(int id)
        {
            return _applicationDbContext.Users.Find(id);
        }

        public List<Permission> GetUserPermissions(int userId)
        {
            return _applicationDbContext.UserPermissions
                .Include(x => x.Permission)
                .Where(x => x.UserId == userId).Select(x => x.Permission).ToList();
        }

        public async Task<int> SaveChanges(CancellationToken cancellationToken)
        {
            return await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
