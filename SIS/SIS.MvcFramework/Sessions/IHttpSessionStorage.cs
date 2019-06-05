namespace SIS.MvcFramework.Sessions
{
    using HTTP.Sessions;

    public interface IHttpSessionStorage
    {
        IHttpSession GetSession(string id);
        bool ContainsSession(string id);
    }
}