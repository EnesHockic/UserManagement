using API.Domain.Entities;

namespace API.Interfaces.Persistence
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        User? GetUserById(int id);
        void Add(User user);
        Task<int> SaveChanges(CancellationToken cancellationToken);
        List<Permission> GetUserPermissions(int userId);
    }
}
