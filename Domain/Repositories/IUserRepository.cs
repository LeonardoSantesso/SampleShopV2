using Domain.Entities;

namespace Domain.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserByLoginAsync(string userName, string password);
    Task<User?> GetUserByUserNameAsync(string userName);
}

