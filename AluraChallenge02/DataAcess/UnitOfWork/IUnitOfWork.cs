using Challenge02.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace Challenge02.DataAcess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IDespesaRepository Despesas { get; }
        IReceitaRepository Receitas { get; }
        IUserRepository Users { get; }
        Task Commit();
        void RollBack();
    }
}
