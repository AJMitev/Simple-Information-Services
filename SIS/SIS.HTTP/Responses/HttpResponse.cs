namespace SIS.HTTP.Responses
{
    using System;
    using System.Text;
    using Common;
    using Contracts;
    using Enums;
    using Extensions;
    using Headers;
    using Headers.Contracts;

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
            CoreValidator.ThrowIfNull(header, nameof(header));
            this.Headers.AddHeader(header);
        }

        public byte[] GetBytes()
        {
            byte[] httpResponseWithoutBody = Encoding.UTF8.GetBytes(this.ToString());

            byte[] httpResponseWithBody = new byte[httpResponseWithoutBody.Length + this.Content.Length];


            for (int i = 0; i < httpResponseWithoutBody.Length; i++)
            {
                httpResponseWithBody[i] = httpResponseWithoutBody[i];

            }

            int bodyLength = Math.Abs(httpResponseWithoutBody.Length - httpResponseWithBody.Length);
            for (int i = 0; i < bodyLength; i++)
            {
                httpResponseWithBody[i + httpResponseWithoutBody.Length] = this.Content[i];
            }

            return httpResponseWithBody;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb
                .Append(
                    $"{GlobalConstants.HttpOneProtocolFragment} {(int)this.StatusCode} {this.StatusCode.GetStatusCodeMessage()}")
                .Append(GlobalConstants.HttpNewLine)
                .Append(this.Headers)
                .Append(GlobalConstants.HttpNewLine);

            sb.Append(GlobalConstants.HttpNewLine);

            return sb.ToString();
        }
    }
}