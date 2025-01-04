using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceApresVente.Models;

public class ApplicationDbContext : IdentityDbContext<Client>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Article> Articles { get; set; }
    public DbSet<Reclamation> Reclamations { get; set; }
    public DbSet<Intervention> Interventions { get; set; }
    public DbSet<PieceDeRechange> Pieces { get; set; }
    public DbSet<ResponsableSAV> Responsables { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Customize the ASP.NET Identity model and override defaults if needed
        // Example: Rename the ASP.NET Identity table names
        builder.Entity<Client>(entity =>
        {
            entity.ToTable("Clients");
        });
    }
}
