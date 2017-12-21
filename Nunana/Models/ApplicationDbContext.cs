using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Nunana.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rental>()
                .ToTable("Rentals");

            modelBuilder.Entity<Rental>()
                .HasKey(t => new { t.RoomId, t.TenantId });

            modelBuilder.Entity<Tenant>()
                .HasMany(r => r.Rentals)
                .WithRequired(t => t.Tenant)
                .HasForeignKey(c => c.TenantId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Room>()
                .HasMany(r => r.Rentals)
                .WithRequired(t => t.Room)
                .HasForeignKey(c => c.RoomId);

            base.OnModelCreating(modelBuilder);
        }
    }
}