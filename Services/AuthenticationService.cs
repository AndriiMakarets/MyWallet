using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AV.ProgrammingWithCSharp.Budgets.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using WalletModel;

namespace AV.ProgrammingWithCSharp.Budgets.Services
{
    public class AuthenticationService
    {
        private readonly DataContext _context;

        public AuthenticationService(DataContext context)
        {
            _context = context;
        }

        public async Task<User?> TryAuthenticateAsync()
        {
            var login = await _context.Authorizations
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Time > DateTime.Now.AddMinutes(-5));
            return login?.User;
        }

        public async Task<User> AuthenticateAsync(AuthUser authUser)
        {
            if (String.IsNullOrWhiteSpace(authUser.Email) || String.IsNullOrWhiteSpace(authUser.Password))
                throw new ArgumentException("Login or Password is Empty");
            var user = await _context.Users.FirstOrDefaultAsync(user =>
                user.Email == authUser.Email);
            if (user is null)
                throw new Exception("No user with this email");
            using var algorithm = SHA256.Create();
            var hashPassword =
                Encoding.UTF8.GetString(algorithm.ComputeHash(Encoding.UTF8.GetBytes(authUser.Password)));
            if (hashPassword != user.HashedPassword)
                throw new Exception("Wrong password");
            var auth = new Authorization()
            {
                Time = DateTime.Now,
                User = user
            };
            await _context.Authorizations.AddAsync(auth);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> RegisterUserAsync(RegisterUser regUser)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(t => t.Email == regUser.Email);
            if (dbUser != null)
                throw new Exception("User already exists");
            if (String.IsNullOrWhiteSpace(regUser.Email) || String.IsNullOrWhiteSpace(regUser.Password) ||
                String.IsNullOrWhiteSpace(regUser.LastName) || string.IsNullOrWhiteSpace(regUser.FirstName))
                throw new ArgumentException("Login, Password or Last Name is Empty");
            using var algorithm = SHA256.Create();
            dbUser = new User()
            {
                FirstName = regUser.FirstName,
                LastName = regUser.LastName,
                Email = regUser.Email,
                HashedPassword =
                    Encoding.UTF8.GetString(algorithm.ComputeHash(Encoding.UTF8.GetBytes(regUser.Password)))
            };
            await _context.Users.AddAsync(dbUser);
            await _context.SaveChangesAsync();
            return dbUser;
        }
    }
}