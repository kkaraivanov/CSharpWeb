namespace WebBasics.ServerFramework.Attributes
{
    using System;
    using HttpServer;

    public abstract class BaseHttpAttribute : Attribute
    {
        public string Url { get; set; }

        public abstract HttpMethod Method { get; }
    }
}