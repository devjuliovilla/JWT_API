using JWT_API.DTO;
using JWT_API.Entities;
using JWT_API.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWT_API.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AppDbContext dbContext;
        private readonly IPasswordHasher<User> passwordHasher;
        private readonly JWT jwt;

        public AccountController(AppDbContext dbContext,
            IPasswordHasher<User> passwordHasher,
            IOptions<JWT> jwt)
        {
            this.dbContext = dbContext;
            this.passwordHasher = passwordHasher;
            this.jwt = jwt.Value;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequest)
        {
            var result = await LoginAsync(loginRequest);
            if (result == null)
                return Unauthorized();

            return new OkObjectResult(result);
        }

        private async Task<string> LoginAsync(LoginRequestDTO loginRequest)
        {
            try
            {
                var dbUser = await dbContext.Users
                    .Include(x=>x.Role)
                    .FirstOrDefaultAsync(x => x.Username == loginRequest.Username);
                if (dbUser == null)
                {
                    return null;
                }

                var verifyPassword = passwordHasher.VerifyHashedPassword(dbUser, dbUser.Password, loginRequest.Password);
                if (verifyPassword == Microsoft.AspNetCore.Identity.PasswordVerificationResult.Success)
                {
                    var token = GenerateToken(dbUser);
                    return new JwtSecurityTokenHandler().WriteToken(token);
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private JwtSecurityToken GenerateToken(User user)
        {
            var claims = new[]
        {
                                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                new Claim("Role", user.Role.Name)
                        };

            var ssk = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var signinCredentials = new SigningCredentials(ssk, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: jwt.Issuer,
                audience: jwt.Audience,
                claims: claims,
            expires: new DateTime().AddDays(1),
                signingCredentials: signinCredentials);

            return token;
        }
    }
}
