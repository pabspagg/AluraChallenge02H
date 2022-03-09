using Challenge02.Domain.Interfaces;
using Challenge02.Models;
using Challenge02.Repository;
using System.Threading.Tasks;

namespace Challenge02.DataAcess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public IDespesaRepository Despesas { get; private set; }

        public IReceitaRepository Receitas { get; private set; }

        public IUserRepository Users { get; private set; }

        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            Receitas = new ReceitaRepository(_context);
            Despesas = new DespesaRepository(_context);
        }


        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public void RollBack()
        {

        }
    }
}
