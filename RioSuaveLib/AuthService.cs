using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;

namespace RioSuaveLib
{
    public class AuthService
    {
        public AuthService(RioSuaveContext rsContext)
        {
            _rsContext = rsContext;
        }

        public async Task<bool> CanAuthenticateAsync(string username, string password) 
        {
            var user = await _rsContext.Users.SingleOrDefaultAsync(u => u.Username == username);
            
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
