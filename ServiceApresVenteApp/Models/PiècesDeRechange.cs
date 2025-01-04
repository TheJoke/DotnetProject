namespace ServiceApresVente.Models
{
    public class PieceDeRechange
    {
        public int Id { get; set; } 
        public string Nom { get; set; } = string.Empty;
        public string Reference { get; set; } = string.Empty;
        public double Prix { get; set; }
        public int Stock { get; set; }

        // Navigation property
        public ICollection<Intervention> Interventions { get; set; } = new List<Intervention>();
    }
}
