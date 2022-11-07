using Common.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataService.GetDataCacheHttpClient.Repository.RepositoryDb.ConfigurationsModels
{
    internal class EntityMarketInstrumentConfiguration : IEntityTypeConfiguration<MarketInstrument>
    {
        public void Configure(EntityTypeBuilder<MarketInstrument> builder)
        {
            builder.HasKey(stock => stock.Figi);

            builder.HasAlternateKey(stock => stock.Isin);

            builder.Property(stock => stock.MinPriceIncrement);
            builder.Property(stock => stock.Lot);
            builder.Property(stock => stock.Currency);
            builder.Property(stock => stock.Type);

            builder.HasIndex(stock => stock.Ticker)
                .IsUnique();

            builder.HasIndex(stock => stock.Name);

            builder.Property(stock => stock.Name)
                .IsRequired();
        }
    }
}
