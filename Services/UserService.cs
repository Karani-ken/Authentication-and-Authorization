using Authentication_and_Authorization.Data;
using Authentication_and_Authorization.Models;
using Authentication_and_Authorization.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace Authentication_and_Authorization.Services
{
    public class UserService : IUserInterface
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context=context;
        }
        public Task<User> GetUserByEmail(string email)
        {
            return _context.Users.Where(u => u.email.Contains(email)).FirstOrDefaultAsync();

          
        }

        public async Task<string> RegisterUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return "User added successfully";
           
        }
    }
}
