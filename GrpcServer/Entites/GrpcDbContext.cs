using Microsoft.EntityFrameworkCore;

namespace GrpcServer.Entites
{
    public class GrpcDbContext : DbContext
    {
        public GrpcDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> User{ get; set; }
    }
}
