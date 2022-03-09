using Challenge02.DataAcess.UnitOfWork;
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
    public class ResumoController : Controller
    {
        private readonly IUnitOfWork _uow;

        /// <summary>
        /// Construtor da classe ReceitaController
        /// </summary>
        public ResumoController(IUnitOfWork uow) => _uow = uow;

        /// <summary>
        /// Pesquisa receitas em um determinado mês/ano.
        /// </summary>
        [HttpGet]
        [Route("{ano}/{mes}")]
        public async Task<IActionResult> GetResumoByMonthYear(
            [FromRoute] string ano,
            [FromRoute] string mes
        )
        {
            try
            {
                // Seleciona pelo mês/ano
                var receitas = (await _uow.Receitas.GetAll())
                    .Where(i => i.Data.ToString("MM/yyyy") == $"{mes}/{ano}")
                    .Select(i => i.Valor);

                var despesas = (await _uow.Despesas.GetAll())
                    .Where(i => i.Data.ToString("MM/yyyy") == $"{mes}/{ano}")
                    .Select(i => i.Valor);

                // soma por categoria
                var somaPorCategoria = (await _uow.Despesas.GetAll())
                    .Where(i => i.Data.ToString("MM/yyyy") == $"{mes}/{ano}")
                    .GroupBy(t => t.Categoria)
                    .Select(
                        t =>
                            new
                            {
                                Categoria = Enum.GetName(t.Key),
                                NumeroDespesas = t.Count(),
                                Total = t.Sum(ta => ta.Valor),
                            }
                    )
                    .ToList();

                var receitasMensal = receitas.Sum();
                var despesasMensal = despesas.Sum();
                var saldoMensal = receitasMensal - despesasMensal;

                return Ok(new { saldoMensal, receitasMensal, despesasMensal, somaPorCategoria });
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
