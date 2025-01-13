namespace ServiceApresVente.Models
{
    public class Reclamation
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public StatutReclamation Statut { get; set; } = StatutReclamation.EnAttente;
        public DateTime DateReclamation { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public Article? Article { get; set; } 

        public ICollection<Intervention> Interventions { get; set; } = new List<Intervention>();
    }
}
