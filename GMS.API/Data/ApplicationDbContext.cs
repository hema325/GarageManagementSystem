using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GMS.API.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<User> Users { get; private set; }
        public DbSet<Brand> Brands { get; private set; }
        public DbSet<Owner> Owners { get; private set; }
        public DbSet<Vehicle> Vehicles { get; private set; }
    }
}
