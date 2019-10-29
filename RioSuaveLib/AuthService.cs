using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

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
            
            if (user?.Salt == null)
            {
                return false;
            }

            var hashed = HashPassword(password, user.Salt);
            return hashed.SequenceEqual(user.Password);
        }

        private IEnumerable<byte> HashPassword(string password, byte[] salt)
        {
            var hashed = KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA256, iterCount, bitsRequested / 8);
            return hashed;
        }

        private const int iterCount = 10000;
        private const int bitsRequested = 256;

        private readonly RioSuaveContext _rsContext;
    }
}
