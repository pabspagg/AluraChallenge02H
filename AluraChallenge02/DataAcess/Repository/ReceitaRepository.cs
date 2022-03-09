using Challenge02.Domain.Interfaces;
using Challenge02.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Challenge02.Repository
{
    public class ReceitaRepository : GenericRepository<Receita>, IReceitaRepository
    {
        public ReceitaRepository(AppDbContext context) : base(context)
        {
        }

        public IEnumerable<Receita> GetReceitaByMonth(Expression<Func<Receita, bool>> expression)
        {
            return _context.Receitas.Where(expression).ToList();
        }
    }
}
