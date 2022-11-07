namespace DataService.GetDataCacheHttpClient.Repository.Interfaces
{
    public interface IRepository<TEntity, TId> where TEntity : class
    {
        Task<List<TEntity>> GetAll(CancellationToken cancellationToken = default);

        Task<TEntity> GetById(TId id, CancellationToken cancellationToken = default);

        Task<TEntity> Create(TEntity entity, CancellationToken cancellationToken = default);

        Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken = default);

        Task<TEntity> Delete(TId id, CancellationToken cancellationToken = default);
    }
}
