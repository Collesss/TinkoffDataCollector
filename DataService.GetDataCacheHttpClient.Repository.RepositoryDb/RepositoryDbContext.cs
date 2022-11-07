using Common.Data;
using DataService.GetDataCacheHttpClient.Repository.Data;
using DataService.GetDataCacheHttpClient.Repository.RepositoryDb.ConfigurationsModels;
using Microsoft.EntityFrameworkCore;

namespace DataService.GetDataCacheHttpClient.Repository.RepositoryDb
{
    public class RepositoryDbContext : DbContext
    {
        public DbSet<DataAboutAlreadyLoaded> DataAboutAlreadyLoadeds { get; set; }

        public DbSet<MarketInstrument> MarketInstruments { get; set; }

        public DbSet<CandlePayload> CandlePayloads { get; set; }

        public RepositoryDbContext(DbContextOptions<RepositoryDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new EntityMarketInstrumentConfiguration());
            modelBuilder.ApplyConfiguration(new EntityCandlePayloadConfiguration());
            modelBuilder.ApplyConfiguration(new EntityDataAboutAlreadyLoadedConfiguration());
        }
    }
}
