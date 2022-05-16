using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RollCallSystem_MongoDB.Models;
using RollCallSystem_MongoDB.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;


namespace RollCallSystem_MongoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JWTTokenController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly UsersService _UsersService;

        public JWTTokenController(IConfiguration configuration, UsersService UsersService)
        {
            _configuration = configuration;
            _UsersService = UsersService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(LoginUser loginUser)
        {
            if (loginUser != null && loginUser.Email != null && loginUser.Password != null)
            {
                User user = new User()
                {
                    Email = loginUser.Email,
                    Password = loginUser.Password,
                };

                var userData = await GetUser(user.Email, user.Password);
                var jwt = _configuration.GetSection("Jwt").Get<Jwt>();
                if (userData != null)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Id", userData.Id.ToString()),
                        new Claim("Email", userData.Email),
                        new Claim("Password", userData.Password)
                    };

                    //Get all roles from the database (i.e. Teacher, Student)
                    var roles = new List<string>()
                    {
                        "Student", "Teacher", "Admin"
                    };
                    if (roles == null) return NoContent();

                    try
                    {
                        //Get the first role from my role list which I got above, that matches the roleId of the user that has logged in
                        claims.Add(new Claim(ClaimTypes.Role, roles.FirstOrDefault(x => x == userData.Role)));

                        //For ease of access (I don't know how to access the named ones set above lol), I store the user Id in the name type since we're not using that one
                        //If anyone figures out how to access the Claim("Id") later, please let me know <3 <3
                        claims.Add(new Claim(ClaimTypes.Name, userData.Id.ToString()));
                    }
                    catch
                    {
                        return BadRequest("Invalid Credentials");
                    }


                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.key));
                    var signin = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        jwt.Issuer,
                        jwt.Audience,
                        claims.ToArray(),
                        expires: DateTime.Now.AddMinutes(20),
                        signingCredentials: signin
                    );
                    return Ok((new JwtSecurityTokenHandler().WriteToken(token)));
                }
                else
                {
                    return BadRequest("Invalid Credentials");
                }
            }

            else
            {
                return BadRequest("Invalid Credentials");
            }
        }

        private async Task<User> GetUser(string userEmail, string userPassword)
        {
            return await _UsersService.GetAsyncLogin(userEmail, userPassword);
        }
    }
}
