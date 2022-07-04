using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FlightTracker.DataAccess;

namespace FlightTracker.Models
{
    public class Pilot
    {
        public int Id { get; set; }
        public string EmployeeNumber { get => Id.ToString().PadLeft(4, '0'); }

        [Required]
        public string Username { get; set; }

        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        public Role Role { get; set; }

        public string Password { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }

    public static class PilotExtensions
    {
        public static List<Pilot> ToModels(this IEnumerable<DataAccess.Entities.Pilot> pilots)
        {
            if(pilots == null)
            {
                return new List<Pilot>();
            }

            return pilots.Select(p => new Pilot
            {
                Id = p.Id,
                Username = p.Username,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Role = p.Role,
                Password = p.Password,
            }).OrderBy(p=> p.FirstName).ThenBy(p=>p.LastName).ToList();
        }
    }
}
