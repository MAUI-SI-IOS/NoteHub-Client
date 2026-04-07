using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bus.logic.ApiService
{

    public class HttpException: Exception
    {
        public int code;
        public string msg;
        public HttpException(int code, string msg)
        {
            this.code = code;
            this.msg  = msg;
        }
    }
}
