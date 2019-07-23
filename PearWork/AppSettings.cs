using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PearWork
{
    public class AppSettings
    {

        public string JwtKey { get; set; }
        public string JwtExpireDays { get; set; }
        public string JwtIssuer { get; set; }

        public string JwtAudience { get; set; }
    }
}
