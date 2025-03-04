using System.ComponentModel.DataAnnotations;

namespace Maanslogen.Businesslogic.Models;
    public class Manufacturer
    {
        public Manufacturer(string name, string logoSrc, Guid createdBy)
        {
            Name = name;
            LogoSrc = logoSrc;
            CreatedBy = createdBy;
        }
        
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Name { get; set; }

        public string LogoSrc { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
