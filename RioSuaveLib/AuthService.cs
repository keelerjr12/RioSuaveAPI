using System.Linq;

namespace RioSuaveLib
{
    public class AuthService
    {
        public AuthService(RioSuaveContext rsContext)
        {
            _rsContext = rsContext;
        }

        public bool CanAuthenticate(string username, string password)
        {
            var user = _rsContext.Users.SingleOrDefault(u => u.Username == username);

            return user != null && user.Password == password;
        }

        private readonly RioSuaveContext _rsContext;
    }
}
