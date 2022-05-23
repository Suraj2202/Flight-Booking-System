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
    public class LogoutController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] LoginDetails value)
        {
            using (UserSideContext ctx = new UserSideContext())
            {
                LoginDetails user = ctx.LoginDetails?.Where(x =>
                                                      x.UserName == value.UserName &&
                                                      x.Password == value.Password)
                                                      .FirstOrDefault();

                if (user != null)
                {
                    user.Token = "Logout";
                    ctx.SaveChanges();

                    return Ok("Success");
                }
                return BadRequest();
            }
        }
    }
}
