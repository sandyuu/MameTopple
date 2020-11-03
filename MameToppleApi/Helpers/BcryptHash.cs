using MameToppleApi.Interfaces;

namespace MameToppleApi.Helpers
{
    public class BcryptHash : IEncryptionAdapter
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool Verify(string encoded, string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, encoded);
        }
    }
}