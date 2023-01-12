using Microsoft.EntityFrameworkCore;

namespace DataService.GetDataCacheHttpClient.Repository.RepositoryDb.Extensions
{
    public static class DbSetExtensions
    {
        public static async Task Merge<TEntity>(this DbSet<TEntity> dbSet,
            IEnumerable<TEntity> entities, IEqualityComparer<TEntity> entityComparer,
            IEqualityComparer<TEntity> entityKeyComparer,
            CancellationToken cancellationToken = default) where TEntity : class
        {
            //var dbEntities = await dbSet.Where(entity => entities.Any(el => entityKeyComparer.Equals(entity, el))).AsNoTracking().ToListAsync();

            var create = entities.Except(dbSet, entityKeyComparer);
            var update = entities.Except(dbSet, entityComparer).Except(create, entityKeyComparer);

            await dbSet.AddRangeAsync(create, cancellationToken);
            dbSet.UpdateRange(update);
        }
    }
}
