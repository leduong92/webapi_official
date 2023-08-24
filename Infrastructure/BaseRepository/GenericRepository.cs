using System.Data;
using System.Linq.Expressions;
using Core.Entities;
using Core.Interfaces;
using Core.Model;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.BaseRepository;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly TestdbContext context;
    private readonly DbSet<T> dbSet;
    public GenericRepository(TestdbContext context)
    {
        this.context = context;
        this.dbSet = context.Set<T>();
    }
    public async Task<T> GetByIdAsync(object id)
    {
        return await dbSet.FindAsync(id);
    }

    public virtual void Insert(T entity)
    {
        dbSet.Add(entity);
    }

    public virtual void Delete(object id)
    {
        T entityToDelete = dbSet.Find(id);
        Delete(entityToDelete);
    }

    public virtual void Delete(T entityToDelete)
    {
        if (context.Entry(entityToDelete).State == EntityState.Detached)
        {
            dbSet.Attach(entityToDelete);
        }
        dbSet.Remove(entityToDelete);
    }

    public virtual void Update(T entityToUpdate)
    {
        dbSet.Attach(entityToUpdate);
        context.Entry(entityToUpdate).State = EntityState.Modified;
    }

    public async Task<PagedResult<T>> GetWithPaging(int page, int pageSize, Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
    {
        IQueryable<T> query = GetByConditionQueryable(filter, orderBy, includeProperties);
        var result = new PagedResult<T>();
        result.PageIndex = page;
        result.PageSize = pageSize;
        result.TotalCount = query.Count();

        var pageCount = (double)result.TotalCount / pageSize;
        result.NumberOfPage = (int)Math.Ceiling(pageCount);

        var skip = (page - 1) * pageSize;
        result.Results = await query.Skip(skip).Take(pageSize).ToListAsync();
        return result;
    }

    public async Task<IEnumerable<T>> GetEntityWithSpec(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
    {
        return await GetByConditionQueryable(filter, orderBy, includeProperties).ToListAsync();
    }

    private IQueryable<T> GetByConditionQueryable(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
    {
        IQueryable<T> query = dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties.Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        if (orderBy != null)
        {
            return orderBy(query);
        }

        return query;
    }
    public void InsertList(List<T> entities)
    {
        context.Set<T>().AddRange(entities);
        context.SaveChanges();
    }

    public void Delete(Expression<Func<T, bool>> filter = null)
    {
        IQueryable<T> query = dbSet;
        if (filter != null)
        {
            query = query.Where(filter);
        }
        dbSet.RemoveRange(query);
        context.SaveChanges();
    }

    public void Update(List<T> entities)
    {
        foreach (T entity in entities)
        {
            context.Set<T>().Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }
        context.SaveChanges();
    }

    public async Task<IEnumerable<T>> GetWithRawSql(string query, params object[] parameters)
    {
        return await dbSet.FromSqlRaw(query, parameters).ToListAsync();
    }
}
