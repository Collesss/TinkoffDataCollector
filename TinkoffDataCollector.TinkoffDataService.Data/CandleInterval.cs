using System.Runtime.Serialization;

namespace TinkoffDataCollector.TinkoffDataService.Data
{
    public enum CandleInterval
    {
        Minute,
        TwoMinutes,
        ThreeMinutes,
        FiveMinutes,
        TenMinutes,
        QuarterHour,
        HalfHour,
        Hour,
        Day,
        Week,
        Month
    }
}
