using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceApresVente.Models;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Article> Articles { get; set; }
    public DbSet<Reclamation> Reclamations { get; set; }
    public DbSet<Intervention> Interventions { get; set; }
    public DbSet<PieceDeRechange> Pieces { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Customize the ASP.NET Identity model and override defaults if needed
        // Example: Rename the ASP.NET Identity table names
        builder.Entity<User>(entity =>
        {
            entity.ToTable("Users");
        });
    }
}
