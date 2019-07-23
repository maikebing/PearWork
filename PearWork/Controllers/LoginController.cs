using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PearWork.Data;
using PearWork.Dtos;
using PearWork.Extensions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PearWork.Controllers
{
    [Route("api/project/[controller]/[action]")]
    public class LoginController : Controller
    {

        private ApplicationDbContext _context;
        private ILogger _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<IdentityUser> _signInManager;

        public LoginController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration, ILogger<LoginController> logger, ApplicationDbContext context
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _logger = logger;
            _context = context;
        }
        // GET api/<controller>/5
        [AllowAnonymous]
        [HttpPost]
        public ActionResult<ApiResult<string>> GetCaptcha(string mobile)
        {
            string cap = ApiUtils.CreateVerificationText(6);
            this.Set("Captcha", cap);
            return Ok(new ApiResult<string>(cap));
        }

        /*
         * email: mysticboy@live.com
name: 马燕洪
password: 5cb5cd951a41f38bd4626f462eaceb9f
password2: 5cb5cd951a41f38bd4626f462eaceb9f
mobile: 18160209520
captcha: 862570
*/
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<ApiResult<List<string>>>> Register(string email, string name, string password, string password2, string mobile, string captcha)
        {
            ActionResult<ApiResult<List<string>>> actionResult = NoContent();
            List<string> result = new List<string>();
            try
            {
                if (this.Get<string>("Captcha") != captcha?.Trim())
                {
                    result.Add("验证码不正确");
                }
                if (password?.Trim() != password2?.Trim())
                {
                    result.Add("验证密码不正确");
                }
                if (result.Count == 0)
                {
                    var user = new IdentityUser
                    {
                        Email = email,
                        UserName = name,
                        PhoneNumber = mobile
                    };
                    var iduser = await _userManager.CreateAsync(user, password);

                    if (iduser.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, false);
                        await _signInManager.UserManager.AddToRolesAsync(user, new[] { nameof(UserRole.User) });
                        actionResult = Ok(new ApiResult<List<string>>(result));
                    }
                    else
                    {
                        var msg = from e in iduser.Errors select $"{e.Code}:{e.Description}\r\n";
                        msg.ToList().ForEach(a => result.Add(a));
                        actionResult = BadRequest(new ApiResult<List<string>>(ApiCode.CreateUserFailed, string.Join(';', result.ToArray()), result));
                    }
                }
            }
            catch (Exception ex)
            {
                actionResult = this.ExceptionRequest(ex);
                _logger.LogError(ex, ex.Message);
            }
            return actionResult;
        }
    }
}
