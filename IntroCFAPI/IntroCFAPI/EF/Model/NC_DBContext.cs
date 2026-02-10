using Microsoft.EntityFrameworkCore;

namespace IntroCFAPI.EF.Model
{
    public class NC_DbContext : DbContext
    {
        public NC_DbContext(DbContextOptions<NC_DbContext> options)
            : base(options)
        {
        }

        public DbSet<News> News { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
