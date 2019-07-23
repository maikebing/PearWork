using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PearWork.Dtos
{
    public class ApiResult<T>
    {
        public ApiResult(T data)
        {
            Code = 200;
            Msg = "";
            Data = data;
        }
        public ApiResult(int code, string msg, T data)
        {
            Code = code;
            Msg = msg;
            Data = data;
        }
        public ApiResult(ApiCode code, string msg)
        {
            Code = (int)code;
            Msg = msg;
            Data = default(T);
        }
        public ApiResult(ApiCode code)
        {
            Code = (int)code;
            Msg = "";
            Data = default(T);
        }
        public ApiResult(ApiCode code, string msg, T data)
        {
            Code = (int)code;
            Msg = msg;
            Data = data;
        }
        public int Code { get; set; }
        public string Msg { get; set; }
        public T Data { get; set; }
    }
}
