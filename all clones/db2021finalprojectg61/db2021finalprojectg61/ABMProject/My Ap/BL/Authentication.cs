using System.Diagnostics.CodeAnalysis;

namespace My_Ap.BL
{
    public class Authentication
    {
        protected String email;
        protected String password;

        
        public Authentication( String email, String password)
        {
            this.Email=email;
            this.Password=password;
        }
        public Authentication() { }
        public string Email { get => email; set => email = value; }
        public string Password { get => password;   set => password = value; }
    }
}
