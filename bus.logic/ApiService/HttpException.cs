using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bus.logic.ApiService
{
    internal class HttpException: Exception
    {
        public int code; 
        public HttpException(int code)
        {
            this.code = code;
        }
    }
}
