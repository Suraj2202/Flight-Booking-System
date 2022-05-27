using System;
using System.Collections.Generic;

namespace Check_Status_with_PNR.Models
{
    public partial class LoginsDetails
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
    }
}
