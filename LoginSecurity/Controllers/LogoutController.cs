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
        public IActionResult Post([FromBody] string uname)
        {
            using (UserSideContext ctx = new UserSideContext())
            {
                LoginsDetails user = ctx.LoginsDetails?.Where(x =>
                                                      x.UserName == uname)
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
