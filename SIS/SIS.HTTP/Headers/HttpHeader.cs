namespace SIS.HTTP.Headers
{
    using Common;

    public class HttpHeader
    {
        public const string Cookie = "Cookie";
        public const string Location = "Location";
        public const string ContentTypeKey = "Content-Type";

        public HttpHeader(string key, string value)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));
            CoreValidator.ThrowIfNullOrEmpty(value, nameof(value));

            this.Key = key;
            this.Value = value;
        }

        public string Key { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            return $"{this.Key}: {this.Value}";
        }
    }
}