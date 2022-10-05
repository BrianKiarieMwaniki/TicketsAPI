namespace TicketsAPI.Auth
{
    public interface ICustomTokenManager
    {
        string CreateToken(string username);
        string GetUserInfoByToken(string tokenString);
        bool VerifyToken(string token);
    }
}