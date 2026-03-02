using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace bus.logic.ApiService.Url
{
    public class Request
    {
        internal string  Type { get; set; }
        internal string  Uri  { get; set; }
        internal ISerializable? Body { get; set; }
    };
}
