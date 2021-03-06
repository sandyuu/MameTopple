using Isopoh.Cryptography.Argon2;
using MameToppleApi.Interfaces;

namespace MameToppleApi.Helpers
{
    public class Argon2Adapter : IEncryptionAdapter
    {
        public string HashPassword(string password)
        {
            return Argon2.Hash(password);
        }

        public bool Verify(string encoded, string password)
        {
            return Argon2.Verify(encoded, password);
        }
    }
}