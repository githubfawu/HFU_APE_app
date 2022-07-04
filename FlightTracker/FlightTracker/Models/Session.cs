using Newtonsoft.Json;
using System;
using FlightTracker.DataAccess;
using Xamarin.Essentials;

namespace FlightTracker.Models
{
    public class Session
    {
        public string Username  { get; set; }
        public DateTime StartedAt { get; set; }

        public string DisplayName { get; set; }

        public Role UserRole { get; set; }

        public Session() { }

        public Session(string username, DateTime startedAt, string displayName, Role userRole)
        {
            Username = username;
            StartedAt = startedAt;
            DisplayName = displayName;
            UserRole = userRole;
         }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static void EndSession()
        {
            Preferences.Remove(nameof(Session));
        }

        public static void StartSession(string username, string displayName, Role role)
        {
            Preferences.Set(nameof(Session), new Session(username, DateTime.UtcNow, displayName, role).ToString());
        }

        public static Session Get()
        {
            var session = Preferences.Get(nameof(Session), null);
            if (session == null)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<Session>(session);
        }

        public bool IsValid()
        {
            return StartedAt > DateTime.UtcNow.AddHours(-24);
        }
    }
}
