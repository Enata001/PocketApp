using Microsoft.EntityFrameworkCore;
using PocketBook.Core.IRepositories;
using PocketBook.Data;

namespace PocketBook.Core.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected AppDbContext Context;
    protected DbSet<T> DbSet;
    protected readonly ILogger Logger;

    public GenericRepository(AppDbContext context, ILogger logger)
    {
        Context = context;
        Logger = logger;
        DbSet = context.Set<T>();
    }

    public virtual async Task<IEnumerable<T>> GetAll()
    {
        return await DbSet.ToListAsync();
    }

    public virtual async Task<T> GetById(Guid id)
    {
        var results = await DbSet.FindAsync(id);

        return results;
    }

    public virtual async Task<bool> Add(T entity)
    {
        await DbSet.AddAsync(entity);
        return true;
    }

    public virtual Task<bool> Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public virtual Task<bool> Upsert(T entity)
    {
        throw new NotImplementedException();
    }
}