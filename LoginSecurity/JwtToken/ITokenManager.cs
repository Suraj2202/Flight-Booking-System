using LoginSecurity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginSecurity.JwtToken
{
    interface ITokenManager
    {
        string GenerateJsonWebToken(string username);
        bool ValidateToken(string authToken);
    }
}
