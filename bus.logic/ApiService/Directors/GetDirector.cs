using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using bus.logic.ApiService.Url;

namespace bus.logic.ApiService.Directors
{
    internal class GetNoteDirector: IQueryDirector<Note>
    {
        readonly string BaseUrl;
        UrlBuilder builder;

        public GetNoteDirector()
        {
            this.builder = new UrlBuilder();
        }

        public Request MakeQuery()
        {
            return builder.BuildType("GET")
                          .BuildUri(BaseUrl+"")
                          .build();
        }

    }
}
