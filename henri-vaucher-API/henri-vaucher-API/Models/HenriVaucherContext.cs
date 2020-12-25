using Microsoft.EntityFrameworkCore;

namespace henri_vaucher_API.Models
{
    public class HenriVaucherContext : DbContext
    {
        public HenriVaucherContext(DbContextOptions<HenriVaucherContext> options)
            : base(options)
        {
        }

        public DbSet<Picture> Pictures { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

    }
}
