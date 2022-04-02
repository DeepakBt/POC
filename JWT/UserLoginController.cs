using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
namespace JWT
{
    [ApiController]
    [Route("api/UserLogin")]
    public class UserLoginController : ControllerBase
    {
        private IConfiguration configuration;
        public UserLoginController(IConfiguration iConfig)
        {
            configuration = iConfig;
        }
        [Route("~/api/GenToken")]
        [HttpPost]
        public IActionResult GenToken(UserModel ReqLogin)
        {
            string key=configuration.GetValue<string>("Jwt:Key");
            string issuer = configuration.GetValue<string>("Jwt:Issuer");
            string Tokan = string.Empty;
            var userModel = ReqLogin;
            UserRepositoryService ObjRepos = new UserRepositoryService();
            var userDto = ObjRepos.GetUser(userModel);
            if (userDto == null)
            {
                return Ok("Unauth");
            }
            TokenService ObjTServic = new TokenService();


            Tokan = ObjTServic.BuildToken(key, issuer, userDto);
            return Ok(Tokan);
        }
    }
}
