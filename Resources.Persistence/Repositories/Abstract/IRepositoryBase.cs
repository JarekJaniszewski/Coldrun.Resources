namespace Resources.Persistence.Repositories.Abstract
{
    public interface IRepositoryBase<T>
    {
        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);

        Task<int> SaveAsync(CancellationToken cancellationToken);
    }
}
