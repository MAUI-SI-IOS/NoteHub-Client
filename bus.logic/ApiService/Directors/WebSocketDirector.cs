using bus.logic.ApiService.Url;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Runtime.Serialization;
using System.Text;

namespace bus.logic.ApiService.Directors
{
    public class WebSocketDirector: IQueryDirector<ClientWebSocket>
    {
        readonly string BaseUrl;
        UrlBuilder builder;

        public WebSocketDirector()
        {
            this.builder = new UrlBuilder();
        }

        public Request MakeQuery()
        {
            return builder.BuildType("WS")
                          .BuildUri(BaseUrl + "")
                          .build();
        }
    }
}
