using Microsoft.AspNetCore.Mvc;
using PearWork.Dtos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PearWork
{
    public  static class ApiUtils
    {
        public static BadRequestObjectResult ExceptionRequest(this ControllerBase @base, Exception exception)
        {
            MethodBase mb = new StackTrace(exception).GetFrame(0).GetMethod();
            MethodBase cu = new StackTrace(true).GetFrame(0).GetMethod();
            return @base.BadRequest(new
            {
                code = ApiCode.Exception,
                msg = exception.Message,
                data = new
                {
                    ExceptionMethod = mb.DeclaringType.FullName + "." + mb.Name,
                    MethodName = cu.Name
                }
            });
        }
        public static string CreateVerificationText( int length)
        {
            char[] _verification = new char[length];
            char[] _dictionary = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            Random _random = new Random();
            for (int i = 0; i < length; i++) { _verification[i] = _dictionary[_random.Next(_dictionary.Length - 1)]; }
            return  new string(_verification);
        }
    }
}
