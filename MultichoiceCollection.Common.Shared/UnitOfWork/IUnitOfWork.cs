using System;
using MultichoiceCollection.Common.Entities.Base;
using MultichoiceCollection.Models.Repositories.Generic;

namespace MultichoiceCollection.Common.Shared.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();

        TRepository Repository<TEntity, TRepository>() where TEntity : BaseEntity
            where TRepository : GenericRepository<TEntity>;
    }
}