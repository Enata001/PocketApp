using Microsoft.EntityFrameworkCore;
using PocketBook.Core.IConfiguration;
using PocketBook.Data;

namespace PocketBook.Extensions;

public static class PocketExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection collection, IConfiguration configuration)
    {

        var dbConnectionString = configuration.GetConnectionString("DefaultConnection");

        collection.AddDbContext<AppDbContext>(options => options.UseNpgsql(dbConnectionString));
        collection.AddControllers();
        collection.AddEndpointsApiExplorer();
        collection.AddSwaggerGen();
        
        //Adding the unit of work to the DI container
        collection.AddScoped<IUnitOfWork, UnitOfWork>();
        

        return collection;
    }
}