﻿using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceApresVente.Models
{
    public class Article
    {
        public int Id { get; set; } 
        public string Nom { get; set; } = string.Empty;
        public string Reference { get; set; } = string.Empty;
        public DateTime DateAchat { get; set; }
        public int DureeGarantie { get; set; }

        public int UserId { get; set; }

        // Navigation properties
        public User User { get; set; }
        public ICollection<Reclamation> Reclamations { get; set; } = new List<Reclamation>();
    }
}
