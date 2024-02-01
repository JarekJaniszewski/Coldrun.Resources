using MediatR;
using Resources.Common.SafeGuards;
using Resources.Domain.Entities;
using Resources.Domain.Enums;
using Resources.Domain.Models;
using Resources.Persistence.Repositories.Abstract;

namespace Resources.Application.TruckManagement.Queries.TrucksSearch
{
    public class TrucksSearchQueryCommandHandler : IRequestHandler<TrucksSearchQueryCommand, TrucksSearchQueryResponse>
    {
        private readonly ITruckRepository _truckRepository;
        public TrucksSearchQueryCommandHandler(ITruckRepository truckRepository)
        {
            Protector.Against.Null(truckRepository, nameof(truckRepository));

            _truckRepository = truckRepository;
        }

        public async Task<TrucksSearchQueryResponse> Handle(
            TrucksSearchQueryCommand request,
            CancellationToken cancellationToken)
        {
            var v1Request = new TruckSearchQuery
            {
                Filters = request.Filters,
                Sorting = request.Sorting,
                Page = request.Paging.Page,
                PageSize = request.Paging.PageSize
            };

            var trucksTask =  _truckRepository.SearchTrucksAsync(
                v1Request,
                cancellationToken);
            var totalRecordCountTask = _truckRepository.GetTotalRecordCountsAsync(
                v1Request,
                cancellationToken);

            await Task.WhenAll(trucksTask, totalRecordCountTask);

            var trucks = trucksTask.Result;
            var totalRecords = totalRecordCountTask.Result;

            return new TrucksSearchQueryResponse
            {
                Items = MapTruckToResponse(trucks),
                RecordCount = totalRecords,
                PageCount = (int)Math.Ceiling(1d * totalRecords / request.Paging.PageSize)
            };
        }

        private static IEnumerable<TrucksSearchItemDto> MapTruckToResponse(IEnumerable<Truck> trucksCollection)
        {
            return trucksCollection.Select(x => new TrucksSearchItemDto
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                Status = (StatusType)x.Status,
                Description = x.Description
            });
        }
    }
}
