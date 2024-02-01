using Resources.Domain.Entities;
using Resources.Domain.Models;

namespace Resources.Persistence.Repositories.Abstract
{
    public interface ITruckRepository
    {
        Task<Truck> CreateTruckAsync(CreateTruckDto truckDto, CancellationToken cancellationToken);

        Task<bool> IsExistAsync(string truckCode, CancellationToken cancellationToken);

        Task<Truck> GetTruckAsync(string truckCode, CancellationToken cancellationToken);

        Task<Truck> UpdateTruckAsync(Truck truck, CancellationToken cancellationToken);

        Task DeleteTruckAsync(string truckCode, CancellationToken cancellationToken);

        Task<IEnumerable<Truck>> SearchTrucksAsync(TruckSearchQuery searchQuery, CancellationToken cancellationToken);

        Task<int> GetTotalRecordCountsAsync(TruckSearchQuery searchQuery, CancellationToken cancellationToken);
    }
}
