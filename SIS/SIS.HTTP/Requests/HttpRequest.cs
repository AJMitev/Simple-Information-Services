﻿namespace SIS.HTTP.Requests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common;
    using Contracts;
    using Enums;
    using Exceptions;
    using Extensions;
    using Headers;
    using Headers.Contracts;

    public class HttpRequest : IHttpRequest
    {
        public HttpRequest(string requestString)
        {
            CoreValidator.ThrowIfNullOrEmpty(requestString, nameof(requestString));

            this.FromData = new Dictionary<string, object>();
            this.QueryData = new Dictionary<string, object>();
            this.Headers = new HttpHeaderCollection();

            ParseRequest(requestString);
        }

        public string Path { get; private set; }
        public string Url { get; private set; }
        public Dictionary<string, object> FromData { get; }
        public Dictionary<string, object> QueryData { get; }
        public IHttpHeaderCollection Headers { get; }
        public HttpRequestMethod RequestMethod { get; private set; }

        private void ParseRequest(string requestString)
        {
            var splitRequestContent = requestString.Split(new[] { GlobalConstants.HttpNewLine }, StringSplitOptions.None);
            var requestLineParams = splitRequestContent[0].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (!this.IsValidRequestLine(requestLineParams))
            {
                throw new BadRequestException();
            }

            this.ParseRequestMethod(requestLineParams);
            this.ParseRequestUrl(requestLineParams);
            this.ParseRequestPath();

            this.ParseRequestHeaders(splitRequestContent.Skip(1).ToArray());
            this.ParseRequestParameters(splitRequestContent[splitRequestContent.Length - 1]);
        }

        private void ParseRequestQueryParameters()
        {
            var queryString = this.Url.Split(new[] { '?', '#' }).Skip(1).Take(1).ToString();
            var queryParams = queryString.Split('&');

            if (!IsValidRequestQueryString(queryString, queryParams))
            {
                throw new BadRequestException();
            }

            for (int i = 0; i < queryParams.Length; i++)
            {
                var data = queryParams[i].Split('=');
                var key = data[0];
                var val = data[1];

                this.QueryData.Add(key, val);
            }
        }

        private void ParseRequestFormDataParameters(string requestBody)
        {
            var dataParams = requestBody.Split('&');

            for (int i = 0; i < dataParams.Length; i++)
            {
                var data = dataParams[i]?.Split('=');
                if (data != null)
                {
                    var key = data[0];
                    var val = data[1];

                    this.FromData.Add(key, val);
                }
            }
        }

        private void ParseRequestParameters(string requestBody)
        {
            this.ParseRequestQueryParameters();
            this.ParseRequestFormDataParameters(requestBody);
        }

        private void ParseRequestHeaders(string[] rawHeaders)
        {
            for (int i = 0; i < rawHeaders.Length; i++)
            {
                var data = rawHeaders[i].Split(": ");
                var key = data[0];
                var val = data[1];

                var currentHeader = new HttpHeader(key, val);
                this.Headers.AddHeader(currentHeader);
            }
        }

        private void ParseRequestPath()
        {
            this.Path = this.Url.Split('?')[0];

        }

        private void ParseRequestUrl(string[] requestLineParams)
        {
            this.Url = requestLineParams[1];
        }

        private void ParseRequestMethod(string[] requestLineParams)
        {
            bool isMethodValid = Enum.TryParse<HttpRequestMethod>(requestLineParams[0].Capitalize(), out HttpRequestMethod requestMethod);

            if (!isMethodValid)
            {
                throw new BadRequestException(string.Format(GlobalConstants.UnsupportedHttpMethodExceptionMessage, requestLineParams[0]));
            }

            this.RequestMethod = requestMethod;
        }

        private bool IsValidRequestLine(string[] requestLines)
        {
            return requestLines.Length == 3 && requestLines[2] == GlobalConstants.HttpOneProtocolFragment;
        }

        private bool IsValidRequestQueryString(string queryString, string[] queryParameters)
        {
            return !string.IsNullOrEmpty(queryString) && queryParameters.Length >= 1;
        }
    }
}