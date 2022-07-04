using System;
using System.Text;

namespace FlightTracker.DataAccess.Helpers
{
    //Referenz: https://stackoverflow.com/questions/7368136/decoding-from-base64-in-c-sharp/7368168#7368168
    //Wichtig: Es findet keine richtige Verschlüsselung statt. Es wird nur sichergestellt, dass das Passwort auf dem Weg nicht verändert werden kann.
    internal class Obfuscator
    {
        public static string Encrypt(string text)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
        }

        public static string Decrypt(string text)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(text));

        }
    }
}
