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

    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        ITokenManager _tokenManager = new TokenManager();

        // GET: api/<LoginController>
        [HttpGet("{uname}")]
        public string Get(string uname)
        {
            using (UserSideContext ctx = new UserSideContext())
            {
                LoginsDetails user = ctx.LoginsDetails?.Where(x =>
                                                      x.UserName == uname)
                                                      .FirstOrDefault();

                if (user != null)
                {
                    bool validCheck = _tokenManager.ValidateToken(user.Token);
                    if (validCheck)
                    {
                        return "Yes";
                    }
                    else
                    {
                        return "No";
                    }
                }
            }
            return "No" ;
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
        public IActionResult Post([FromBody] LoginsDetails value)
        {
            
            using(UserSideContext ctx = new UserSideContext())
            {
                LoginsDetails user = ctx.LoginsDetails?.Where(x =>
                                                      x.UserName == value.UserName &&
                                                      x.Password == value.Password)
                                                      .FirstOrDefault();

                if(user != null)
                {
                    string userToken = user.Token;
                    bool valid = _tokenManager.ValidateToken(userToken);
                    string role = user.Role;
                    if (valid)
                        return Ok("Success Role " + role);
                    else
                    {
                        // if token expire then save it in db.
                        string token = _tokenManager.GenerateJsonWebToken(user.UserName);
                        user.Token = token;
                        ctx.SaveChanges();
                        return Ok("New Token Generated Role " + role);
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
