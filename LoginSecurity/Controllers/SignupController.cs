using LoginSecurity.JwtToken;
using LoginSecurity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginSecurity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignupController : ControllerBase
    {

        ITokenManager _tokenManager = new TokenManager();

        [HttpPost]
        public IActionResult Post([FromBody] LoginDetails value)
        {
            using (UserSideContext ctx = new UserSideContext())
            {
                string token = _tokenManager.GenerateJsonWebToken(value.UserName);
                LoginDetails login = new LoginDetails()
                {
                    UserName = value.UserName,
                    Name = value.Name,
                    Password = value.Password,
                    Email = value.Email,
                    Token = token,
                    Role = value.Role
                };
            
                ctx.LoginDetails.Add(login);
                ctx.SaveChanges();
                return Ok();
            }
        }
    }
}
