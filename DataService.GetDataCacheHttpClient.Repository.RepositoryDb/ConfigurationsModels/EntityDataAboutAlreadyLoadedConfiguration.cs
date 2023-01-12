using DataService.GetDataCacheHttpClient.Repository.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataService.GetDataCacheHttpClient.Repository.RepositoryDb.ConfigurationsModels
{
    public class EntityDataAboutAlreadyLoadedConfiguration : IEntityTypeConfiguration<DataAboutAlreadyLoaded>
    {
        void IEntityTypeConfiguration<DataAboutAlreadyLoaded>.Configure(EntityTypeBuilder<DataAboutAlreadyLoaded> builder)
        {
            builder.HasKey(dataAboutLoaded => new { dataAboutLoaded.Figi, dataAboutLoaded.Interval, dataAboutLoaded.Time });

            builder
                .HasOne<MarketInstrument>()
                .WithMany()
                .HasForeignKey(dataAboutLoaded => dataAboutLoaded.Figi)
                .HasPrincipalKey(stock => stock.Figi)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
