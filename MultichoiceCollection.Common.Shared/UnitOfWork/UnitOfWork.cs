using System;
using System.Collections.Generic;
using MultichoiceCollection.Common.Entities.Base;
using MultichoiceCollection.Models.Repositories.Context;
using MultichoiceCollection.Models.Repositories.Generic;

namespace MultichoiceCollection.Common.Shared.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _context;
        private bool _disposed;
        private Dictionary<string, object> _repositories;

        public UnitOfWork(AppDbContext context)
        {
            this._context = context;
        }

        public int SaveChanges()
        {
            return this._context.SaveChanges();
        }

        public TRepository Repository<TEntity,TRepository>() where TEntity : BaseEntity where TRepository : GenericRepository<TEntity>
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<string, object>();
            }
            var type = typeof(TRepository).Name;
            if (!_repositories.ContainsKey(type))
            {
                var repositoryInstance = Activator.CreateInstance(typeof(TRepository), _context);
                _repositories.Add(type, repositoryInstance);
            }
            return (TRepository)_repositories[type];
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                    _context = null;
                }
                    
            }
            _disposed = true;
        }
    }
}