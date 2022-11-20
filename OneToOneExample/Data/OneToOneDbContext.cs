using Microsoft.EntityFrameworkCore;

namespace OneToOneExample.Data
{
    public class OneToOneDbContext : DbContext
    {
        public OneToOneDbContext(DbContextOptions<OneToOneDbContext> options)
            : base(options)
        {
        }

        public DbSet<OneToOneExample.Models.User> User { get; set; } = default!;

        public DbSet<OneToOneExample.Models.UserActivation> UserActivation { get; set; }
    }
}
