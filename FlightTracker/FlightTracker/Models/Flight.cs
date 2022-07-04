using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightTracker.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Distance { get; set; }
        public virtual Pilot Owner { get; set; }
        public TimeSpan Duration { get; set; }
        public string Comments { get; set; }
    }

    public static class FlightExtensions
    {
        public static List<Flight> ToModels(this IEnumerable<DataAccess.Entities.Flight> flights)
        {
            return flights.Select(f => new Flight
            {
                Id = f.Id,
                Comments = f.Comments,
                Date = f.Date,
                Distance = f.Distance,
                Duration = f.Duration,
                Owner = new Pilot
                {
                    FirstName = f.Owner.FirstName,
                    Id = f.Owner.Id,
                    LastName = f.Owner.LastName,
                    Role = f.Owner.Role,
                    Username = f.Owner.Username,
                }
            }).OrderByDescending(f => f.Date).ToList();
        }
    }
}