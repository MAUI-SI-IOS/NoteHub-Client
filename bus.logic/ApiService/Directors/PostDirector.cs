using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using bus.logic.ApiService.Url;

namespace bus.logic.ApiService.Directors
{
    internal class PostNoteDirector: IQueryDirector
    {
        readonly string BaseUrl;
        UrlBuilder builder;
        ISerializable body;

        public PostNoteDirector(ISerializable body)
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
