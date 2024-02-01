using Microsoft.EntityFrameworkCore;
using Resources.Domain.Entities;
using Resources.Domain.Models;
using Resources.Persistence.Repositories.Abstract;
using System.Linq.Dynamic.Core;


namespace Resources.Persistence.Repositories
{
    public class TruckRepository : RepositoryBase<Truck>, ITruckRepository
    {
        public TruckRepository(ColdrunEntityDbContext context) : base(context)
        {
        }
        public async Task<bool> IsExistAsync(string truckCode, CancellationToken cancellationToken)
        {
            var exist = await Context.Trucks.AnyAsync(t => t.Code.ToLower().Equals(truckCode.ToLower()), cancellationToken); ;
            return exist;
        }


        public async Task<Truck> CreateTruckAsync(CreateTruckDto truckDto, CancellationToken cancellationToken)
        {
            var truck = new Truck
            {
                Code = truckDto.Code,
                Name = truckDto.Name,
                Status = (byte)truckDto.Status,
                Description = truckDto.Description

            };
            Create(truck);
            await SaveAsync(cancellationToken);
            return truck;
        }

        public async Task<Truck> GetTruckAsync(string truckCode, CancellationToken cancellationToken)
        {
            var exist = await Context.Trucks.FirstAsync(t => t.Code.ToLower().Equals(truckCode.ToLower()), cancellationToken);
            return exist;
        }

        public async Task<Truck> UpdateTruckAsync(Truck truck, CancellationToken cancellationToken)
        {
            Update(truck);
            await SaveAsync(cancellationToken);
            return truck;   
        }

        public async Task DeleteTruckAsync(string truckCode, CancellationToken cancellationToken)
        {
            var truck = await Context.Trucks.FirstAsync(t => t.Code.ToLower().Equals(truckCode.ToLower()), cancellationToken);
            Delete(truck);
            await SaveAsync(cancellationToken);
        }

        public async Task<IEnumerable<Truck>> SearchTrucksAsync(TruckSearchQuery searchQuery, CancellationToken cancellationToken)
        {
            var offSet = SafeCreateOffset(searchQuery.Page, searchQuery.PageSize);
            var result = Context.Trucks.Where(searchQuery.Filters).OrderBy(searchQuery.Sorting).Skip(offSet).Take(searchQuery.PageSize);
            var result2 = Context.Trucks.Where(searchQuery.Filters).OrderBy(searchQuery.Sorting);
            return await result.ToListAsync(cancellationToken);
        }

        public async Task<int> GetTotalRecordCountsAsync(TruckSearchQuery searchQuery, CancellationToken cancellationToken)
        {
            var result = await Context.Trucks.Where(searchQuery.Filters).Distinct().CountAsync(cancellationToken);
            return  result;
        }

        private static int SafeCreateOffset(int page, int pageSize)
        {
            var offSet = (page - 1) * pageSize;

            return offSet;
        }

    }
}
