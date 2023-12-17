using PocketBook.Models;

namespace PocketBook.Core.IRepositories;

public interface IUserRepository: IGenericRepository<User>
{
    // Task<String> GetFirstNameAndLastName(Guid id);
}