using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Challenge02.Controllers
{
    public interface IRepositoryBase<T>
    {
        Task<IActionResult> GetAsync();

        Task<IActionResult> GetByIdAsync([FromRoute] int id);

        Task<IActionResult> PostAsync([FromBody] T model);

        Task<IActionResult> PutAsync([FromBody] T model, [FromRoute] int id);

        Task<IActionResult> DeleteAsync([FromRoute] int id);
    }
}