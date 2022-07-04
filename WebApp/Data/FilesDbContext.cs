using Microsoft.EntityFrameworkCore;

namespace WebApp.Data
{
    public class FilesDbContext: DbContext
    {
        public FilesDbContext(DbContextOptions<FilesDbContext> options)
            : base(options)
        {
        }

        public DbSet<WebApp.Models.FileModel>? File { get; set; }
    }
}
