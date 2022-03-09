using Challenge02.Domain.Interfaces;
using Challenge02.Models;

namespace Challenge02.Repository
{
    public class DespesaRepository : GenericRepository<Despesa>, IDespesaRepository
    {
        public DespesaRepository(AppDbContext context) : base(context)
        {
        }
    }
}
