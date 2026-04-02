using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using bus.logic.ApiService.Url;
using bus.logic.service;

namespace bus.logic.ApiService.Directors
{
    internal class GetDirector<T>: IQueryDirector<T>
    {
        string route;
        UrlBuilder builder;
        
        public GetDirector(string route)
        {
            this.builder = new UrlBuilder();
            this.route = route;
        }

        public Request MakeQuery()
        {
            return builder.BuildType("GET")
                          .BuildUri(this.route)
                          .build();
        }

    }
}
