namespace TicketsAccess.Repository.APIClient
{
    public interface IWebAPIExecuter
    {
        Task InvokeDelete<T>(string uri);
        Task<T> InvokeGet<T>(string uri);
        Task<T> InvokePost<T>(string uri, T obj);
        Task InvokePut<T>(string uri, T obj);
    }
}