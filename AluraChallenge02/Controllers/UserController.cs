using Challenge02.DataAcess.UnitOfWork;
using Challenge02.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge02.Controllers
{
    [ApiController]
    [Route("api/v1/user")]
    [AllowAnonymous]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _uow;
        public UserController(IUnitOfWork uow) => _uow = uow;


        /// <summary>
        ///  Exibe todos usuários.
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid) return BadRequest("Modelo inválido. Não é permitido campos em branco.");

            try
            {
                var usuarios = await _uow.Users.GetAll();
                return Ok(usuarios);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }


        /// <summary>
        ///  Cadastro usuário.
        /// </summary>
        [HttpPost]
        [Route("cadastro")]
        public async Task<IActionResult> CreateAsync([FromBody] User model)
        {
            if (!ModelState.IsValid) return BadRequest("Modelo inválido. Não é permitido campos em branco.");

            var modelo = new User(model.Username, model.Password, "user_role");

            try
            {
                var usuarios = (await _uow.Users.GetAll()).Where(i => i.Username == model.Username).FirstOrDefault();
                if (usuarios != null)
                {
                    await _uow.Users.Add(modelo);
                    await _uow.Commit();
                    return Ok(modelo);
                }
                return BadRequest("Não é possível adicionar usuário existente");
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}