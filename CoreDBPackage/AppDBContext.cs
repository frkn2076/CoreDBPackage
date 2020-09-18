using CoreDBPackage.Model;
using Microsoft.EntityFrameworkCore;

namespace CoreDBPackage {
    public class AppDBContext : DbContext {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) {
        }
        public DbSet<Login> Login { get; set; }
    }
}
