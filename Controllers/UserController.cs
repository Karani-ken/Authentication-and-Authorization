using Authentication_and_Authorization.Models;
using Authentication_and_Authorization.Request;
using Authentication_and_Authorization.Services.IServices;
using AutoMapper;
using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Authentication_and_Authorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserInterface _userInterface;
        private readonly IConfiguration _configuration;
        public UserController(IMapper mapper,IUserInterface userInterface, IConfiguration configuration)
        {
            _mapper = mapper;
            _userInterface = userInterface;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Register(AddUser newUser)
        {
            var user = _mapper.Map<User>(newUser);
            //hash password
            user.password = BCrypt.Net.BCrypt.HashPassword(user.password);
           // user.Role = "Admin";
            var res = await _userInterface.RegisterUser(user);
            return Ok(res);
        }
        //login
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(logUser loginUser)
        {
            //check if user email exists
            var existingUser = await _userInterface.GetUserByEmail(loginUser.email);
            if(existingUser == null)
            {
                return NotFound("Invalid credentials");
            }
            //verify password
            var isPasswordValid = BCrypt.Net.BCrypt.Verify(loginUser.password, existingUser.password);
            if (!isPasswordValid)
            {
                return NotFound("Invalid credentials");
            }
            //right credentials provided

            //create token
            var token = CreateToken(existingUser);
            return Ok(token); 
        }
        //create a token
        private string CreateToken(User user)
        {
            //key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("TokenSecurity:SecretKey")));
            //signing credentials
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //payload-data
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("Names", user.name));
            claims.Add(new Claim("Sub", user.id.ToString()));
            claims.Add(new Claim("Role", user.Role));
            //create token
            var tokenGenerated = new JwtSecurityToken(
                _configuration["TokenSecurity:Issuer"],
                _configuration["TokenSecurity:Audience"],
                signingCredentials:credentials,
                claims:claims,
                expires:DateTime.UtcNow.AddHours(2)
                );
            var token = new JwtSecurityTokenHandler().WriteToken(tokenGenerated);
            return token;
        }
    }
}
