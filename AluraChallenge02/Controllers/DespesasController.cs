using AutoMapper;
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
    [Route("api/v1/[controller]")]
    [Authorize]
    public class despesasController : Controller
    {
        private readonly IUnitOfWork _uow;

        /// <summary>
        /// Construtor da classe ReceitaController
        /// </summary>
        public despesasController(IUnitOfWork uow) => _uow = uow;

        /// <summary>
        /// Retorna todas as despesas.
        /// </summary>
        /// <response code="200">Retorna todas as despesas cadastradas.</response>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var resultado = (await _uow.Despesas.GetAll())
                    .Select(
                        t =>
                            new
                            {
                                Id = t.Id,
                                Descricao = t.Descricao,
                                Valor = t.Valor,
                                Data = t.Data,
                                Categoria = Enum.GetName(t.Categoria),
                            }
                    )
                    .ToList();
                ;
                return Ok(resultado);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Retorna despesa por id.
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            try
            {
                var resultado = await _uow.Despesas.GetById(id);
                if (resultado == null)
                    return NotFound();
                return resultado == null ? NotFound() : Ok(resultado);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Pesquisa despesa por descrição.
        /// </summary>
        /// <response code="200">Retorna a despesa selecionada pelo id.</response>
        /// <response code="404">Despesa não encontrada.</response>
        /// <response code="500">Erro no servidor.</response>
        ///
        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> GetByDescription([FromQuery] string query)
        {
            try
            {
                var resultado = (await _uow.Despesas.GetAll())
                    .Where(i => i.Descricao.Contains(query))
                    .ToList();
                return resultado == null ? NotFound() : Ok(resultado);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Pesquisa despesas em um determinado mês/ano.
        /// </summary>
        [HttpGet]
        [Route("{ano}/{mes}")]
        public async Task<IActionResult> GetByMonthYear(
            [FromRoute] string ano,
            [FromRoute] string mes
        )
        {
            try
            {
                var resultado = (await _uow.Despesas.GetAll()).Where(
                    i => i.Data.ToString("MM/yyyy") == $"{mes}/{ano}"
                );
                if (resultado == null)
                    return NotFound();
                return Ok(resultado);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Adiciona uma única despesa.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Despesa model)
        {
            //Verifica a validade da Url e se o Modelo é válido
            if (!ModelState.IsValid)
                return BadRequest("Modelo inválido. Não é permitido campos em branco.");

            try
            {
                // Verifica se já existe despesa
                var resultado = (await _uow.Despesas.GetAll())
                    .Where(
                        i =>
                            CheckDescriptionSameMonth(
                                i.Data,
                                model.Data,
                                i.Descricao,
                                model.Descricao
                            )
                    )
                    .FirstOrDefault();

                if (resultado != null)
                    return BadRequest("Não é possível adicionar despesas iguais no mesmo mês.");

                // Cria nova despesa a ser adicionada
                var modelo = new Despesa(
                    model.Descricao,
                    Convert.ToDecimal(model.Valor),
                    model.Data
                );
                //Adiciona despesa e salva
                await _uow.Despesas.Add(modelo);
                await _uow.Commit();
                return Ok(modelo);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Verifica se existe a mesma despesa em um mês.
        /// </summary>
        /// <returns>Boolean</returns>
        private bool CheckDescriptionSameMonth(
            DateTime data1,
            DateTime data2,
            String descricao1,
            String descricao2
        )
        {
            return data1.ToString("MM/yy") == data2.ToString("MM/yy") && descricao1 == descricao2;
        }

        /// <summary>
        /// Atualiza despesa.
        /// </summary>
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> PutAsync([FromBody] Despesa model, [FromRoute] int id)
        {
            //Verifica se o Modelo é válido
            if (!ModelState.IsValid)
                return BadRequest("Modelo inválido");

            try
            {
                var resultado = await _uow.Despesas.GetById(id);
                if (resultado == null)
                    return NotFound("Despesa não existe.");
                resultado.Descricao = model.Descricao;
                resultado.Valor = model.Valor;
                resultado.Data = model.Data;
                _uow.Despesas.Update(resultado);
                await _uow.Commit();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Deleta despesa por id.
        /// </summary>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            try
            {
                var resultado = await _uow.Despesas.GetById(id);
                if (resultado == null)
                    return NotFound("Modelo não existe");
                _uow.Despesas.Remove(resultado);
                await _uow.Commit();
                return Ok(resultado);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
