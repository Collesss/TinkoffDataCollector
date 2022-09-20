using TinkoffDataCollector.SaveService.Data;

namespace TinkoffDataCollector.SaveService.Interfaces
{
    public interface ISaveService
    {
        void Save(SaveServiceData saveServiceData);
    }
}
