using System;
using System.ComponentModel.DataAnnotations;


namespace FlightTracker.DataAccess.Entities
{
    public class Flight : IEntity
    {
        public int Id { get;set; }
        public DateTime Date { get; set; }
        public int Distance { get; set; }
        public Pilot Owner { get; set; }
        public TimeSpan Duration { get; set; }

        [MaxLength(200)]
        public string? Comments { get; set; }
    }
}
