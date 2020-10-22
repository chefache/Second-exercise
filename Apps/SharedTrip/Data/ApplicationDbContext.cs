namespace SharedTrip.Data
{
    using Microsoft.EntityFrameworkCore;
    using System.Security.Cryptography.X509Certificates;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions db)
            : base(db)
        {

        }




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-AMLLGJ5;Database=SharedTrip;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTrip>().HasKey(ut => new 
            {
                ut.UserId, ut.TripId 
            });
        }
    }
}
