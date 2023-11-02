using GenericContracts.Contracts;
using GenericTools.Repositories;
using Microsoft.EntityFrameworkCore;
using User.Core.Entities;
using User.Repository.Db;

namespace User.Repository.Repositories
{
    public class UserRepository : GenericRepository<AppUser>
    {
        public UserRepository(UsersContext dbContext) : base(dbContext)
        {

        }

    }
}