namespace SIS.HTTP.Headers
{
    using System.Collections.Generic;
    using System.Text;
    using Common;

    public class HttpHeaderCollection : IHttpHeaderCollection
    {
        private readonly Dictionary<string, HttpHeader> headers;

        public HttpHeaderCollection()
        {
            this.headers = new Dictionary<string, HttpHeader>();
        }

        public void AddHeader(HttpHeader header)
        {
            this.headers.Add(header.Key, header);
        }

        public bool ContainsHeader(string key)
        {
            return this.headers.ContainsKey(key);
        }

        public HttpHeader GetHeader(string key)
        {
            return this.headers[key];
        }

        public override string ToString()
        {
            var result = new StringBuilder();

            foreach (var header in headers)
            {
                result.Append(header.Value + GlobalConstants.HttpNewLine);
            }

            return result.ToString().TrimEnd();
        }
    }
}