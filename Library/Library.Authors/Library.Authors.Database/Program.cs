using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Library.Authors.Database
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            var ctxFactory = new ApplicationDbContextFactory();

            await using var context = ctxFactory.CreateDbContext(new[] {""});

            await context.Database.MigrateAsync();
        }
    }
}
