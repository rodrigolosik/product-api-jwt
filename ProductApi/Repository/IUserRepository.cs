using System.Collections.Generic;
using System.Threading.Tasks;
using ProductApi.Models;

namespace ProductApi.Repository
{
    public interface IUserRepository
    {
        void Add(User user);
        void Update(User user);
        void Delete(User user);
        Task<IEnumerable<User>> ListUsers();
        Task<User> GetUser(string username, string password);
        Task<User> GetUser(int id);
        Task<bool> SaveChangesAsync();
    }
}
