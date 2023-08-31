using Authentication_and_Authorization.Models;

namespace Authentication_and_Authorization.Services.IServices
{
    public interface IUserInterface
    {
        Task<string> RegisterUser(User user);

        Task<User> GetUserByEmail(string email);
    }
}
