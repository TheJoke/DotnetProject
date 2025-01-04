using Microsoft.AspNetCore.Identity;

namespace ServiceApresVente.Models
{
    public class Client : IdentityUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Adress { get; set; } = string.Empty;
        public string TelNumber { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;


        // Navigation property
        public ICollection<Reclamation> Reclamations { get; set; } = new List<Reclamation>();
    }


}
