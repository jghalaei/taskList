using System.Reflection;
using Microsoft.EntityFrameworkCore;
using GenericContracts.Common;
using GenericContracts.Contracts;
using System.Linq.Expressions;
namespace GenericTools.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : EntityBase
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _entities;
        public GenericRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _entities = _dbContext.Set<T>();
            ArgumentNullException.ThrowIfNull(_entities);
        }
        public async Task<T> InsertAsync(T entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            _entities.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;

        }

        public async Task<T> UpdateAsync(T entity)
        {
            var foundEntity = await GetByIdAsync(entity.Id);
            ArgumentNullException.ThrowIfNull(foundEntity);
            Type typ = entity.GetType();
            foreach (PropertyInfo prop in typ.GetProperties())
            {
                if (prop.Name == "Id" || prop.Name == "CreatedBy" || prop.Name == "CreatedAt" || prop.Name == "ModifiedBy" || prop.Name == "ModifiedAt")
                    continue;
                prop.SetValue(foundEntity, prop.GetValue(entity));
            }
            foundEntity.ModifiedAt = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<bool> DeleteAsync(Guid Id)
        {
            var deletedEntity = await GetByIdAsync(Id);
            ArgumentNullException.ThrowIfNull(deletedEntity);
            _entities.Remove(deletedEntity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _entities.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
        {
            var data = await _entities.Where(predicate).AsNoTracking().ToListAsync();
            return data;
        }

        public async Task<T?> GetByIdAsync(Guid Id)
        {
            return await _entities.FindAsync(Id);
        }

        public async Task<T?> GetOneAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {

                var user = await _entities.Where(predicate).FirstOrDefaultAsync();
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }


    }


}