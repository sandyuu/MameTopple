using Isopoh.Cryptography.Argon2;
using MameToppleApi.Interfaces;

namespace MameToppleApi.Helpers
{
    public class Argon2Adapter : IArgon2Adapter
    {
        public bool Verify(string encoded, string password)
        {
            return Argon2.Verify(encoded, password);
        }
    }
}