using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PocketBook.Core.IConfiguration;
using PocketBook.Core.IRepositories;
using PocketBook.Core.Repositories;
using PocketBook.Models;

namespace PocketBook.Data;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly AppDbContext _context;
    private readonly ILogger _logger;
    public IUserRepository Users { get; private set; }

    public UnitOfWork(AppDbContext context, ILoggerFactory logger)
    {
        _logger = logger.CreateLogger("Logs");
        _context = context;
        Users = new UserRepository(_context, _logger);
    }

    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }

    public  void Dispose()
    {
         _context.DisposeAsync();
    }
    // public async void Dispose()
    // {
    //     await _context.DisposeAsync();
    // }
}