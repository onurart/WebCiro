using System.Linq.Expressions;
using WebCiro.Core.Utilities.Abstract;
using WebCiro.Core.Utilities.Concrete;

namespace WebCiro.Core.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IDataResult<T>> GetByIdAsync(int id);
        IQueryable<T> GetAll();
        Task<IDataResult<List<T>>> GetListAsync();
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task<IDataResult<T>> AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        IDataResult<T> Update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        Task<Result> RemoveSoft(T entity);
        Task<IDataResult<List<T>>> GetListTestAsync();
    }
}
