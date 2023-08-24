using System.Collections;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.BaseRepository;
using Infrastructure.Data;

namespace Infrastructure.BaseRepository;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly TestdbContext _context;
    private Hashtable _repositories;
    public UnitOfWork(TestdbContext context)
    {
        _context = context;
    }

    public async Task<int> Complete()
    {
        return await _context.SaveChangesAsync();
    }

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
    {
        if (_repositories == null) _repositories = new Hashtable();

        var type = typeof(TEntity).Name;

        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(GenericRepository<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);

            _repositories.Add(type, repositoryInstance);
        }

        return (IGenericRepository<TEntity>)_repositories[type];
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        this.disposed = true;
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}

