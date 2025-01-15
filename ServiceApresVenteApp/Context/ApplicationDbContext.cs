using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceApresVente.Models;
using Microsoft.Extensions.Hosting;
using System.Reflection.Emit;
using ServiceApresVenteApp.Models;

public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
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
        builder.Entity<Intervention>()
            .HasMany(e => e.PiecesUtilisees)
            .WithMany(e => e.Interventions)
            .UsingEntity<IntervensionPiece>(j => j.Property(e => e.Quantite));
    }
}
