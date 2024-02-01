using Microsoft.EntityFrameworkCore;
using Resources.Common.SafeGuards;
using Resources.Persistence.Repositories.Abstract;
using System;

namespace Resources.Persistence.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RepositoryBase(ColdrunEntityDbContext context)
        {
            Protector.Against.Null(context, nameof(context));

            Context = context;
        }
        public ColdrunEntityDbContext Context { get; }

        public void Create(T entity)
        {
            Context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            Context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
        }
        public Task<int> SaveAsync(CancellationToken cancellationToken)
        {
            return Context.SaveChangesAsync(cancellationToken);
        }
    }
}
