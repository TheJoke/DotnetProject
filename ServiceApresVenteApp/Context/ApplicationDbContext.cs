using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceApresVente.Models;

public class ApplicationDbContext : IdentityDbContext
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
}
