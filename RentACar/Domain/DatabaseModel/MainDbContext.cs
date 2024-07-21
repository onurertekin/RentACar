using DatabaseModel.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatabaseModel
{
    public class MainDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Rezervation> Rezervations { get; set; }
        public DbSet<CarModel> CarModels { get; set; }


        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>()
         .HasOne(c => c.CarModel)
         .WithMany()
         .HasForeignKey(c => c.CarModelId);

            base.OnModelCreating(modelBuilder);
        }
    }
}