using AutoMapper;
using BusinessObjects;
using DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using Repositories;
using Repositories.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BE_PRN231_CatDogLover.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        private readonly IMapper mapper;
        private IAccountRepository accountRepository;
        public AuthController(IConfiguration configuration, IMapper mapper)
        {
            Configuration = configuration;
            this.mapper = mapper;
            accountRepository = new AccountRepository();
        }

        [AllowAnonymous]
        [HttpPost("LoginUser")]
        public IActionResult LoginUser([FromBody] LoginRequest login)
        {
            IActionResult response = Unauthorized();
            Account account = accountRepository.GetAll().SingleOrDefault(a => a.Email == login.Email && a.Password == login.Password && a.Role.RoleName == "user");
            if (account != null)
            {
                LoginRespone loginRespone = GenerateToken(account.Role.RoleName);
                loginRespone.Account = mapper.Map<AccountDTO>(account);
                loginRespone.Account.Password = null;
                response = Ok(loginRespone);
            }
            return response;
        }
        [AllowAnonymous]
        [HttpPost("LoginStaff")]
        public IActionResult LoginStaff([FromBody] LoginRequest login)
        {
            IActionResult response = Unauthorized();
            Account account = accountRepository.GetAll().SingleOrDefault(a => a.Email == login.Email && a.Password == login.Password && a.Role.RoleName == "staff");
            if (account != null)
            {
                LoginRespone loginRespone = GenerateToken(account.Role.RoleName);
                loginRespone.Account = mapper.Map<AccountDTO>(account);
                loginRespone.Account.Password = null;
                response = Ok(loginRespone);
            }
            return response;
        }
        [AllowAnonymous]
        [HttpPost("LoginAdmin")]
        public IActionResult LoginAdmin([FromBody] LoginRequest login)
        {
            IActionResult response = Unauthorized();
            if (login.Email == Configuration["AdminAccount:Email"] && login.Password == Configuration["AdminAccount:Password"]) { 
                LoginRespone loginRespone = GenerateToken("admin");
                response = Ok(loginRespone);
            }
            return response;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<AccountDTO>> RegisterUser(RegisterRequest register)
        {
            if (!ModelState.IsValid) return BadRequest("Data invalid");
            if (register.PasswordConfirm != register.Password)
            {
                return BadRequest("Password not match with confirm");
            }
            if (register.Email == Configuration["AdminAccount:Email"] || accountRepository.GetAll().Any(a => a.Email.ToLower().Trim() == register.Email.ToLower().Trim()))
            {
                return BadRequest("Email are already exist");
            }
            Account account = new Account();
            try
            {              
                account = mapper.Map<Account>(register);
                account.CreateDate = DateTime.Now;
                account.RoleId = 1;
                account.Status = true;
                accountRepository.AddAccount(account);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Created("", mapper.Map<AccountDTO>(account));
        }

        [HttpGet("RefreshToken/{refreshToken}")]
        public IActionResult RefreshToken(string refreshToken)
        {
            var jwtKey = Encoding.ASCII.GetBytes(Configuration["jwtKey"]);
            var refreshKey = Encoding.ASCII.GetBytes(Configuration["jwtKeyRefresh"]);

            var tokenHandler = new JwtSecurityTokenHandler();

            // Kiểm tra tính hợp lệ của Refresh Token
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(refreshKey)
            };

            try
            {
                SecurityToken validatedToken;
                var principal = tokenHandler.ValidateToken(refreshToken, validationParameters, out validatedToken);

                tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(principal.Claims),
                    Expires = DateTime.UtcNow.AddMinutes(15), // Thời hạn của Access Token
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(jwtKey), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var accessToken = tokenHandler.WriteToken(token);
                return Ok(accessToken);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu Refresh Token không hợp lệ
                return BadRequest("Invalid Refresh Token");
            }
        }
        private LoginRespone GenerateToken(string Role)
        {
            var jwtKey = Encoding.ASCII.GetBytes(Configuration["jwtKey"]);
            var refreshKey = Encoding.ASCII.GetBytes(Configuration["jwtKeyRefresh"]);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.Role, Role),
                }),
                Expires = DateTime.UtcNow.AddMinutes(15), // Thời hạn của Access Token
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(jwtKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(token);

            var refreshDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.Role, Role),
                }),
                Expires = DateTime.UtcNow.AddHours(1), // Thời hạn của Refresh Token
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(refreshKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var refresh = tokenHandler.CreateToken(refreshDescriptor);
            var refreshToken = tokenHandler.WriteToken(refresh);

            return new LoginRespone
            {
                Role = Role,
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
    }
}
