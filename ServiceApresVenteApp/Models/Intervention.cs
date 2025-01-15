using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceApresVente.Models
{
    public class Intervention
    {
        public int Id { get; set; }
        public int ReclamationId { get; set; }
        public string? Technicien { get; set; }
        public DateTime? DateIntervention { get; set; } = DateTime.UtcNow;
        public bool EstSousGarantie { get; set; }
        public double? CoutMainOeuvre { get; set; }
        public double? CoutTotal { get; set; } = 0;

        // Navigation properties
        public Reclamation? Reclamation { get; set; }
        public ICollection<PieceDeRechange>? PiecesUtilisees { get; set; } = new List<PieceDeRechange>();
    }
}
