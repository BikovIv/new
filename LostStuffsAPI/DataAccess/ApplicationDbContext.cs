using System.Data.Entity;

namespace LostStuffs.Models
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
           : base("DefaultConnection")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<LostStuffs.Entities.LostStuff> LostStuffs { get; set; }

        public System.Data.Entity.DbSet<LostStuffs.Entities.Comment> Comments { get; set; }

    }
}