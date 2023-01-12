using DataService.GetDataCacheHttpClient.Repository.Exceptions;
using DataService.GetDataCacheHttpClient.Repository.Interfaces;
using DataService.GetDataCacheHttpClient.Repository.RepositoryDb.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataService.GetDataCacheHttpClient.Repository.RepositoryDb.Implementations
{
    public abstract class Repository<TEntity, VId, EDbConxtet, EEntityEqualityComparer, KEntityKeyEqualityComparer> : IRepository<TEntity, VId> 
        where TEntity : class
        where EDbConxtet : DbContext
        where EEntityEqualityComparer : class, IEqualityComparer<TEntity>, new()
        where KEntityKeyEqualityComparer : class, IEqualityComparer<TEntity>, new()
    {
        protected readonly EDbConxtet _dbConxtet;
        private readonly ILogger<Repository<TEntity, VId, EDbConxtet, EEntityEqualityComparer, KEntityKeyEqualityComparer>> _logger;

        protected Repository(EDbConxtet dbConxtet, ILogger<Repository<TEntity, VId, EDbConxtet, EEntityEqualityComparer, KEntityKeyEqualityComparer>> logger)
        {
            _dbConxtet = dbConxtet ?? throw new ArgumentNullException(nameof(dbConxtet));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        virtual protected async Task<TEntity> Find(VId id, CancellationToken cancellationToken = default) =>
            await _dbConxtet.FindAsync<TEntity>(id, cancellationToken);

        public async Task<TEntity> Create(TEntity entity, CancellationToken cancellationToken = default)
        {
            TEntity result;

            try
            {
                _logger.LogTrace($"Adding entity ${entity}.");

                result = (await _dbConxtet.AddAsync(entity, cancellationToken)).Entity;
                await _dbConxtet.SaveChangesAsync(cancellationToken);

                _logger.LogInformation($"Entity added: {result}.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error adding entity: {entity}.");

                throw new RepositoryException($"Error adding entity: {entity}. See inner exception.", e);
            }

            return result;
        }

        public async Task<TEntity> Delete(VId id, CancellationToken cancellationToken = default)
        {
            TEntity result;

            try
            {
                _logger.LogTrace($"Remove entity with Id: ${id}.");

                TEntity entity = await Find(id, cancellationToken);

                result = _dbConxtet.Remove(entity).Entity;
                await _dbConxtet.SaveChangesAsync(cancellationToken);

                _logger.LogInformation($"Entity removed: {result}.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error remove entity with Id: {id}.");

                throw new RepositoryException($"Error remove entity with Id: {id}. See inner exception.", e);
            }

            return result;
        }

        public async Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken = default) =>
            await _dbConxtet.Set<TEntity>().ToListAsync(cancellationToken);

        public async Task<TEntity> GetById(VId id, CancellationToken cancellationToken = default)
        {
            TEntity result;

            try
            {
                _logger.LogTrace($"Find entity with Id: ${id}.");

                result = await Find(id, cancellationToken);

            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error find entity with Id: {id}.");

                throw new RepositoryException($"Error find entity with Id: {id}. See inner exception.", e);
            }

            return result;
        }

        public async Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken = default)
        {
            TEntity result;

            try
            {
                _logger.LogTrace($"Updating entity ${entity}.");

                result = _dbConxtet.Update(entity).Entity;
                await _dbConxtet.SaveChangesAsync(cancellationToken);

                _logger.LogInformation($"Entity updated: {result}.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error update entity: {entity}.");

                throw new RepositoryException($"Error update entity: {entity}. See inner exception.", e);
            }

            return result;
        }

        public async Task<IEnumerable<TEntity>> Merge(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogTrace($"Merge entities ${entities}.");

                await _dbConxtet.Set<TEntity>().Merge(entities, new EEntityEqualityComparer(), new KEntityKeyEqualityComparer(), cancellationToken);

                await _dbConxtet.SaveChangesAsync(cancellationToken);

                _logger.LogInformation($"Entities merged: {entities}.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error merge entities: {entities}.");

                throw new RepositoryException($"Error merge entities: {entities}. See inner exception.", e);
            }

            return entities;
        }
    }
}
