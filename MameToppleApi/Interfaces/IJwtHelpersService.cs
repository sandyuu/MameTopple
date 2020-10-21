namespace MameToppleApi.Interfaces
{
    public interface IJwtHelpersService
    {
        string GenerateToken(string account, int expireMinutes = 30);
    }
}