using LoginSecurity.JwtToken;
using LoginSecurity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LoginSecurity.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        ITokenManager _tokenManager = new TokenManager();

        // GET: api/<LoginController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Login Successfull redirecting to Booking Page" };
        }

        /*// GET api/<LoginController>/5
        [HttpGet("{uname}")]
        public string Get(string uname)
        {
            return GenerateJsonWebToken(uname);
        }
*/
        // POST api/<LoginController>
        [HttpPost]
        public IActionResult Post([FromBody] LoginDetails value)
        {
            
            using(UserSideContext ctx = new UserSideContext())
            {
                LoginDetails user = ctx.LoginDetails?.Where(x =>
                                                      x.UserName == value.UserName &&
                                                      x.Password == value.Password)
                                                      .FirstOrDefault();

                if(user != null)
                {
                    string userToken = user.Token;
                    bool valid = _tokenManager.ValidateToken(userToken);
                    int role = user.Role;
                    if (valid)
                        return Ok("Success " + role);
                    else
                    {
                        // if token expire then save it in db.
                        string token = _tokenManager.GenerateJsonWebToken(user.UserName);
                        user.Token = token;
                        return Ok("New Token Generated " + role);
                    }
                }
                else
                {
                    return NotFound("User not found");
                }
            }
            
            
        }

        // PUT api/<LoginController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LoginController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
