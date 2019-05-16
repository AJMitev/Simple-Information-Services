namespace SIS.HTTP.Responses
{
    using System.Text;
    using Common;
    using Enums;
    using Headers;

    public class HttpResponse : IHttpResponse
    {
        public HttpResponse()
        {
            this.Headers = new HttpHeaderCollection();
            this.Content = new byte[0];
        }

        public HttpResponse(HttpResponseStatusCode statusCode)
            : this()
        {
            CoreValidator.ThrowIfNull(statusCode, nameof(statusCode));
            this.StatusCode = statusCode;
        }

        public HttpResponseStatusCode StatusCode { get; set; }
        public IHttpHeaderCollection Headers { get; }
        public byte[] Content { get; set; }
        public void AddHeader(HttpHeader header)
        {
            CoreValidator.ThrowIfNull( header, nameof(header));
            this.Headers.AddHeader(header);
        }

        public byte[] GetBytes()
        {
            throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb
                .Append(
                    $"{GlobalConstants.HttpOneProtocolFragment} {(int) this.StatusCode} {this.StatusCode.ToString()}")
                .Append(GlobalConstants.HttpNewLine)
                .Append(this.Headers)
                .Append(GlobalConstants.HttpNewLine);

                sb.Append(GlobalConstants.HttpNewLine);

            return sb.ToString();
        }
    }
}