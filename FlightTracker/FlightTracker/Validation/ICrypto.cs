
namespace FlightTracker.Validation
{
    public interface ICrypto
    {
        public string Encrypt(string clearText);
        public string Decrypt(string cipherText);
    }
}
