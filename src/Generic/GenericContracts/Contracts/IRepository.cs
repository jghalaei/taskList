using System.Linq.Expressions;
using GenericContracts.Common;

namespace GenericContracts.Contracts;

public interface IRepository<T> where T : EntityBase
{
    Task<T> InsertAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<bool> DeleteAsync(Guid Id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
    Task<T?> GetByIdAsync(Guid Id);
    Task<T?> GetOneAsync(Expression<Func<T, bool>> predicate);

}