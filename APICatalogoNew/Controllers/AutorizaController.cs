using APICatalogoNew.Configurations;
using APICatalogoNew.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APICatalogoNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorizaController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly TokenConfiguration _token;

        public AutorizaController(UserManager<IdentityUser> userManager,
                                  SignInManager<IdentityUser> signInManager,
                                  IOptions<TokenConfiguration> token)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _token = token.Value;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            return $"AutorizaController :: Acessado em : {DateTime.Now.ToLongDateString()}";
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] CreateUsuarioDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            return Ok(GerarToken(model.Email));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUsuarioDto model)
        {
            var result = await _signInManager.PasswordSignInAsync(
                model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Login inválido...");
                return BadRequest(ModelState);
            }

            return Ok(GerarToken(model.Email));
        }

        private TokenUsuarioDto GerarToken(string email)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, email),
                new Claim("meuvalor","valor qualquer"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            //gera uma chave com base em um algoritmo simetrico
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_token.Key));

            //gera a assinatura digital do token usando o algoritmo Hmac e a chave private
            var crendenciais = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //Tempo de expiração do token           
            var expiration = DateTime.UtcNow.AddHours(_token.ExpireHours);

            //classe que representa um token JWT e gera o token
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _token.Issuer,
                audience: _token.Audience,
                claims: claims,
                expires: expiration,
                signingCredentials: crendenciais);

            return new TokenUsuarioDto
            {
                Authenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration,
                Message = "Token JWT OK"
            };

        }
    }
}
