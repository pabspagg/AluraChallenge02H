using Challenge02.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Challenge02.Domain.Interfaces
{
    public interface IReceitaRepository : IGenericRepository<Receita>
    {
        IEnumerable<Receita> GetReceitaByMonth(Expression<Func<Receita, bool>> expression);
    }
}
