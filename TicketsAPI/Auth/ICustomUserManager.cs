namespace TicketsAPI.Auth
{
    public interface ICustomUserManager
    {
        string Authenticate(string username, string password);
    }
}