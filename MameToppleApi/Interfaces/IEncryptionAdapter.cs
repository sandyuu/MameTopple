namespace MameToppleApi.Interfaces
{
    public interface IEncryptionAdapter
    {
        string HashPassword(string password);
        bool Verify(string encoded, string password);
    }
}