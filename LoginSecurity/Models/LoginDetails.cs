namespace LoginSecurity.Models
{
    public partial class LoginDetails
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public int Role { get; set; }
    }
}
