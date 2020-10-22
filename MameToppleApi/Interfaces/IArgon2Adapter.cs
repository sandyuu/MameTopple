namespace MameToppleApi.Interfaces
{
    public interface IArgon2Adapter
    {
        bool Verify(string encoded, string password);
    }
}