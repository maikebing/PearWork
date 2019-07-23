using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PearWork.Dtos
{
    public enum ApiCode
    {
       OK=200,
       NotFound=505,
        CreateUserFailed = 506,
        Exception = 507
    }
    public enum UserRole
    {
        Guest,
        User,
        Admin
    }
}
