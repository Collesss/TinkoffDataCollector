using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DataService.GetDataCacheHttpClient.Repository.Data;

namespace DataService.GetDataCacheHttpClient.Repository.RepositoryDb.ConfigurationsModels
{
    public class EntityCandlePayloadConfiguration : IEntityTypeConfiguration<CandlePayload>
    {
        public void Configure(EntityTypeBuilder<CandlePayload> builder)
        {
            builder.HasKey(candle => new { candle.Figi, candle.Time, candle.Interval });

            builder.Property(candle => candle.Open);
            builder.Property(candle => candle.Close);
            builder.Property(candle => candle.High);
            builder.Property(candle => candle.Low);
            builder.Property(candle => candle.Volume);

            builder
                .HasOne<MarketInstrument>()
                .WithMany()
                .HasForeignKey(candle => candle.Figi)
                .HasPrincipalKey(stock => stock.Figi)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
