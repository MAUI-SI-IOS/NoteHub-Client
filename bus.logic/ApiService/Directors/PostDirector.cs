using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using bus.logic.ApiService.Url;
using bus.logic.service;

namespace bus.logic.ApiService.Directors
{
    internal class PostNoteDirector<T>: IQueryDirector<T>
    {
        readonly string BaseUrl;
        UrlBuilder builder;
        Note body;

        public PostNoteDirector(Note body)
        {
            this.builder = new UrlBuilder();
            this.body = body;
        }

        public Request MakeQuery()
        {
            return builder.BuildType("POST")
                          .BuildUri(BaseUrl+"")
                          .buildBody(body)
                          .build();
        }
    }
}
