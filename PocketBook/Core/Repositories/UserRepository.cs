using Microsoft.EntityFrameworkCore;
using PocketBook.Core.IRepositories;
using PocketBook.Data;
using PocketBook.Models;

namespace PocketBook.Core.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext context, ILogger logger) : base(context, logger)
    {
    }

    public override async Task<IEnumerable<User>> GetAll()
    {
        try
        {
            return await DbSet.ToListAsync();
        }
        catch (Exception e)
        {
            Logger.LogError(e, "{Repo} GetAll method error", typeof(UserRepository));
            return new List<User>();
        }
    }

    public override async Task<bool> Upsert(User entity)
    {
        try
        {
            var existingUser = await DbSet.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (existingUser is null)
            {
                return await Add(entity);
            }

            existingUser.FirstName = entity.FirstName;
            existingUser.LastName = entity.LastName;
            existingUser.Email = entity.Email;

            return true;
        }
        catch (Exception e)
        {
            Logger.LogError(e, "{Repo} Upsert method error", typeof(UserRepository));
            return false;
        }
    }


    public override async Task<bool> Delete(Guid id)
    {
        try
        {
            var existingUser = await DbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (existingUser is not null)
            {
                DbSet.Remove(existingUser);
                return true;
            }

            return false;
        }
        catch (Exception e)
        {
            Logger.LogError(e, "{Repo} Delete method error", typeof(UserRepository));
            return false;
        }
    }

    // public Task<string> GetFirstNameAndLastName(Guid id)
    // {
    //     throw new NotImplementedException();
    // }
}