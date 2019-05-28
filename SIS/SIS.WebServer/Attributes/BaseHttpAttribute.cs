﻿namespace SIS.WebServer.Attributes
{
    using System;
    using HTTP.Enums;

    public abstract class BaseHttpAttribute : Attribute
    {
        public string ActionName { get; set; }

        public string Url { get; set; }

        public abstract HttpRequestMethod Method { get; }
    }
}
