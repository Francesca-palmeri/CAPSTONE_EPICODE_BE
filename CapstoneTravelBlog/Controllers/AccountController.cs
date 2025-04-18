
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CapstoneTravelBlog.DTOs;
using CapstoneTravelBlog.Models.Account;
using CapstoneTravelBlog.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CapstoneTravelBlog.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly Jwt _jwtSettings;

        public AccountController(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
         SignInManager<ApplicationUser> signInManager,
        IConfiguration configuration,
        IOptions<Jwt> jwtSettings)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _jwtSettings = jwtSettings.Value;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            if (User.Identity.IsAuthenticated == true) 
            {
                return BadRequest(new { message = "Sei già loggato. Non puoi registrarti di nuovo." });
            }

            var existingUser = await _userManager.FindByEmailAsync(registerRequestDto.Email);
            if (existingUser != null)
            {
                return BadRequest(new { message = "Email già registrata" });
            }

            var newUser = new ApplicationUser()
            {   Email = registerRequestDto.Email,
                UserName = registerRequestDto.Username,
                FirstName = registerRequestDto.FirstName,
                LastName = registerRequestDto.LastName,
                BirthDate = registerRequestDto.BirthDate,
                PhoneNumber = registerRequestDto.PhoneNumber,
                AvatarUrl = "https://media.istockphoto.com/id/1337144146/vector/default-avatar-profile-icon-vector.jpg?s=612x612&w=0&k=20&c=BIbFwuv7FxTWvh5S3vB6bkT0Qv8Vn8N5Ffseq84ClGI="

            };

            var result = await _userManager.CreateAsync(newUser, registerRequestDto.Password);
            if (!result.Succeeded)
            {
     
                return BadRequest(new { message = "Creazione utente fallita", errors = result.Errors });
            }

            await _userManager.AddToRoleAsync(newUser, "User");

            return Ok();
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            if (User?.Identity?.IsAuthenticated == true) 
            {
                return BadRequest(new { message = "Sei già loggato." });
            }

            var user = await _userManager.FindByEmailAsync(loginRequestDto.Email);

            if (user == null)
            {
                return Unauthorized("Invalid email or password");
            }

            var result = await _signInManager.PasswordSignInAsync(user, loginRequestDto.Password, false, false);

            if (!result.Succeeded)
            {
                return Unauthorized("Invalid email or password");
            }

            var roles = await _signInManager.UserManager.GetRolesAsync(user);

            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecurityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(_jwtSettings.ExpiresInDays);

            var token = new JwtSecurityToken(_jwtSettings.Issuer, _jwtSettings.Audience, claims, expires: expiry, signingCredentials: creds);

            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new TokenResponseDto()
            {
                Token = tokenString,
                Expires = expiry
            });
        }

        [HttpGet("profile")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetProfile()
        {
            // Ottiene l'ID dell'utente dal token JWT
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Nessun utente associato al token.");

            // Recupera dal DB l'utente
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound("Utente non trovato.");

            var profileDto = new
            {
                user.Id,
                user.FirstName,
                user.LastName,
                user.Email,
                user.BirthDate,
                user.PhoneNumber,
                user.AvatarUrl
            };

            return Ok(profileDto);
        }
        [HttpPut("profile/avatar")]
        [Authorize]
        public async Task<IActionResult> UpdateAvatar([FromBody] string newAvatarUrl)
        {
            if (string.IsNullOrWhiteSpace(newAvatarUrl))
                return BadRequest(new { message = "URL non valido." });

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound("Utente non trovato.");

            user.AvatarUrl = newAvatarUrl;
            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded
                ? Ok(new { avatarUrl = user.AvatarUrl })
                : BadRequest(new { message = "Errore durante l'aggiornamento dell'avatar." });
        }
        [HttpDelete("profile/avatar")]
        [Authorize]
        public async Task<IActionResult> DeleteAvatar()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound("Utente non trovato.");

            if (string.IsNullOrWhiteSpace(user.AvatarUrl))
                return BadRequest(new { message = "Nessun avatar da eliminare." });

            user.AvatarUrl = null;
            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded
                ? Ok(new { message = "Avatar rimosso." })
                : BadRequest(new { message = "Errore durante la rimozione dell'avatar." });
        }

    }

}



