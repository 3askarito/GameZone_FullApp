using GamerZone.MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GamerZone.MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().ToTable("Users", "security");
            builder.Entity<IdentityRole>().ToTable("Roles", "security");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "security");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "security");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "security");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "security");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "security");
            builder.Entity<ApplicationUser>().HasQueryFilter(u => !u.IsDeleted);

            builder.Entity<Category>().HasData(new Category[]
            {
                new Category{Id = 1, Name ="Sports"},
                new Category{Id = 2, Name = "Action" },
                new Category{Id = 3, Name = "Adventure"},
                new Category{Id = 4, Name = "Racing"},
                new Category{Id = 5, Name = "Fighting" },
                new Category{Id = 6, Name = "Film"}
            });
            builder.Entity<Device>().HasData(new Device[]
            {
                new Device{Id = 1, Name = "Playstation", Icon = "bi bi-playstation"},
                new Device{Id = 2, Name = "xbox", Icon = "bi bi-xbox"},
                new Device{Id = 3, Name = "Nintendo Switch", Icon = "bi bi-nintendo-switch"},
                new Device{Id = 4, Name = "PC", Icon = "bbi bi-pc-display"}
            });
            builder.Entity<GameDevice>().HasKey(x => new { x.GameId, x.DeviceId });
            builder.Entity<ApplicationUserGame>().HasKey(x => new { x.ApplicationUserId, x.GameId });
        }
        public DbSet<Game> Games {  get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<GameDevice> GameDevices { get; set; }
        public DbSet<ApplicationUserGame> ApplicationUsersGames { get; set; }
    }
}
