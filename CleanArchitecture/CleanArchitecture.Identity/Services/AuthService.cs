using CleanArchitecture.Application.Constants;
using CleanArchitecture.Application.Contracts.Identity;
using CleanArchitecture.Application.Models.Identity;
using CleanArchitecture.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanArchitecture.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<AuthResponse> Login(AuthRequest authRequest)
        {
            var user = await _userManager.FindByEmailAsync(authRequest.Email);

            if(user is null)
                throw new Exception($"El usuario con email {authRequest.Email} no existe");

            var response = await _signInManager.PasswordSignInAsync(user.UserName, authRequest.Password, false, lockoutOnFailure: false);

            if (!response.Succeeded)
                throw new Exception($"Las credenciales son icorrectas");

            var token = await GenerateToken(user);

            var authResponse = new AuthResponse
            {
                Id = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Email = user.Email,
                UserName = user.UserName
            };

            return authResponse;
        }

        public async Task<RegistrationResponse> Register(RegistrationRequest registrationRequest)
        {
            var existingUser = await _userManager.FindByNameAsync(registrationRequest.UserName);

            if (existingUser is not null)
                throw new Exception($"El userName ya se utiliza en otra cuenta");

            var existingEmail = await _userManager.FindByNameAsync(registrationRequest.Email);

            if (existingEmail is not null)
                throw new Exception($"El email ya se utiliza en otra cuenta");

            var userNew = new ApplicationUser
            {
                Email = registrationRequest.Email,
                Nombre = registrationRequest.Nombre,
                Apellidos = registrationRequest.Apellidos,
                UserName = registrationRequest.UserName,
                EmailConfirmed = true
            };

            var response = await _userManager.CreateAsync(userNew, registrationRequest.Password);

            if (response.Succeeded)
            {
                await _userManager.AddToRoleAsync(userNew, "Operation");

                var token = await GenerateToken(userNew);

                return new RegistrationResponse
                {
                    Email = userNew.Email,
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    UserId = userNew.Id,
                    UserName = userNew.UserName
                };
            }

            throw new Exception($"{response.Errors}");
            
        }

        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser applicationUser)
        {
            var userClaims = await _userManager.GetClaimsAsync(applicationUser);
            var roles = await _userManager.GetRolesAsync(applicationUser);

            var rolesClaims = new List<Claim>();

            foreach (var role in roles)
            {
                rolesClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, applicationUser.UserName),
                new Claim(JwtRegisteredClaimNames.Email, applicationUser.Email),
                new Claim(CustomClaimTypes.Uid, applicationUser.Id)
            }.Union(userClaims).Union(rolesClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
    }
}
