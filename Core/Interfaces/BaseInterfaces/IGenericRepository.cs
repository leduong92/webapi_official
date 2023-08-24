using System.Linq.Expressions;
using Core.Entities;
using Core.Model;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(object id);
        Task<IEnumerable<T>> GetEntityWithSpec(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");

        Task<IEnumerable<T>> GetWithRawSql(string query, params object[] parameters);
        Task<PagedResult<T>> GetWithPaging(int pageIndex = 1,
            int pageSize = 10, Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");
        void Insert(T entity);
        void InsertList(List<T> entities);
        void Update(T entity);
        void Update(List<T> entities);
        void Delete(T entity);
        void Delete(object id);
        void Delete(Expression<Func<T, bool>> filter = null);
    }
}