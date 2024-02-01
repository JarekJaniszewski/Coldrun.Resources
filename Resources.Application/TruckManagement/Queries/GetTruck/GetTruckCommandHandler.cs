using MediatR;
using Resources.Common.SafeGuards;
using Resources.Domain.Entities;
using Resources.Domain.Enums;
using Resources.Persistence.Repositories.Abstract;

namespace Resources.Application.TruckManagement.Queries.GetTruck
{
    public class GetTruckCommandHandler : IRequestHandler<GetTruckCommand, GetTruckResponse>
    {
        private readonly ITruckRepository _truckRepository;
        public GetTruckCommandHandler(ITruckRepository truckRepository)
        {
            Protector.Against.Null(truckRepository, nameof(truckRepository));

            _truckRepository = truckRepository;
        }

        public async Task<GetTruckResponse> Handle(
            GetTruckCommand request,
            CancellationToken cancellationToken)
        {
            var isExist = await _truckRepository.IsExistAsync(request.Code, cancellationToken);
            if (!isExist)
                throw new ArgumentException($"Truck code {request.Code} don't exist in database");

            var truck = await _truckRepository.GetTruckAsync(request.Code, cancellationToken);

            return MapTruckToResponse(truck);
        }

        private static GetTruckResponse MapTruckToResponse(Truck truckCommand)
        {
            return new GetTruckResponse
            {
                Id = truckCommand.Id,
                Code = truckCommand.Code,
                Name = truckCommand.Name,
                Status = (StatusType)truckCommand.Status,
                Description = truckCommand.Description
            };
        }
    }
}
