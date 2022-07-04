using FlightTracker.DataAccess.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlightTracker.DataAccess.Entities
{
    public class Pilot : IEntity
    {
        private string? password;

        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string? Username { get; set; }

        [MaxLength(255)]
        public string? FirstName { get; set; }

        [Required]
        [MaxLength(255)]
        public string? LastName { get; set; }
        public Role Role { get; set; }

        [Required, MaxLength(50)]
        public string Password 
        { 
            get =>  Obfuscator.Decrypt(password); 
            set => password = Obfuscator.Encrypt(value); 
        }

        public virtual ICollection<Flight> Flights { get; set; } = new List<Flight>();
    }
}
