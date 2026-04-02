using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace bus.logic.ApiService.Url
{
    public class UrlBuilder
    {
        Request request = new Request();
        public UrlBuilder BuildType(string type)
        {
            request.Type = type;
            return this;
        }
        public UrlBuilder BuildUri(string uri)
        {
            request.Uri = uri;
            return this;
        }
        public UrlBuilder buildBody(object body)
        {
            request.Body = body;
            return this;
        }
        public Request build()
        {
            var build = request;
            request = new Request();
            return build;
        }
    }
}
