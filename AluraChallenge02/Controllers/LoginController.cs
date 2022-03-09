using Challenge02.DataAcess.UnitOfWork;
using Challenge02.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Challenge02.Controllers
{
    [ApiController]
    [Route("api/v1/login")]
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IUnitOfWork _uow;

        /// <summary>
        /// Construtor da classe LoginController
        /// </summary>
        public LoginController(IConfiguration config, IUnitOfWork uow)
        {
            _config = config;
            _uow = uow;
        }

        /// <summary>
        ///  Login do usuário.
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult<dynamic> Authenticate([FromBody] User model)
        {
            var user = _uow.Users.Find(x => x.Username == model.Username && x.Password == model.Password);
            if (user == null) return Unauthorized(new { message = "Usuário ou senha inválidos" });

            var token = GenerateToken(model);
            return new {
                token = token
            };
        }

        private string GenerateToken(User user)

        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
                }),

                Expires = DateTime.UtcNow.AddHours(Convert.ToDouble(_config.GetValue<String>("JwtToken:TokenExpiry"))),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config.GetValue<String>("JwtToken:SecretKey"))), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}