namespace SIS.HTTP.Requests.Contracts
{
    using System.Collections.Generic;
    using Cookies.Contracts;
    using Enums;
    using Headers;
    using Headers.Contracts;
    using SIS.HTTP.Sessions.Contracts;

    public interface IHttpRequest
    {
        string Path { get; }
        string Url { get; }
        Dictionary<string, object> FormData { get; }
        Dictionary<string, object> QueryData { get; }
        IHttpHeaderCollection Headers { get; }
        IHttpCookieCollection Cookies { get; }
        HttpRequestMethod RequestMethod { get; }
        IHttpSession Session { get; set; }
    }
}