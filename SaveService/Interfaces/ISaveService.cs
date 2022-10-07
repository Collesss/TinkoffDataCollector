using SaveService.Data;

namespace SaveService.Interfaces
{
    public interface ISaveService
    {
        Task Save(SaveData saveData, CancellationToken cancellationToken);
    }
}
