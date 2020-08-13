using _43LimeMobileApp.Models.Entities;
using System.Data.Entity;

namespace _43LimeMobileApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("DefaultConnection")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<ButtonCommand> Commands { get; set; }
        public virtual DbSet<ButtonCommandCategory> CommandCategories { get; set; }
        public virtual DbSet<CommandLog> CommandLogs { get; set; }
    }
}
