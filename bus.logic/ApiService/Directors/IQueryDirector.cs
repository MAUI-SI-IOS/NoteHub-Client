using bus.logic.ApiService.Url;
using System;
using System.Collections.Generic;
using System.Text;

namespace bus.logic.ApiService.Directors
{
    public interface IQueryDirector
    {
        Request MakeQuery();
    }
}
