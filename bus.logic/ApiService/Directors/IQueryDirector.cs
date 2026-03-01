using bus.logic.ApiService.Url;
using System;
using System.Collections.Generic;
using System.Text;

namespace bus.logic.ApiService.Directors
{
    internal interface IQueryDirector
    {
        Request MakeQuery();
    }
}
