using Microsoft.EntityFrameworkCore;
using SQLite.Web.API.Entities;

namespace SQLite.Web.API.Services
{
    public class EFCoreRepository: DbContext, IRepository
    {
        public DbSet<User> Users { get; set; }

        public EFCoreRepository(DbContextOptions<EFCoreRepository> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .ToTable("Users")
                .HasKey(u => u.Id);

            modelBuilder.Entity<User>()
                .HasData(new User[]
                {
                    new User
                    {
                        Id = 1,
                        Email = "user_1@somewhere.com",
                        DisplayName = "User 1",
                    },
                    new User
                    {
                        Id = 2,
                        Email = "user_2@somewhere.com",
                        DisplayName = "User 2",
                    },
                    new User
                    {
                        Id = 3,
                        Email = "user_3@somewhere.com",
                        DisplayName = "User 3",
                    }
                });
        }

        public IEnumerable<User> GetUsers()
        {
            return this.Users;
        }
    }
}
