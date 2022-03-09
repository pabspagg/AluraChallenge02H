using Challenge02.Domain.Interfaces;
using Challenge02.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Challenge02.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }
    }
}
