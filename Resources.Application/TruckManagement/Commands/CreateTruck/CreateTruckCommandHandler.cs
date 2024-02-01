using MediatR;
using Resources.Common.SafeGuards;
using Resources.Domain.Entities;
using Resources.Domain.Enums;
using Resources.Domain.Models;
using Resources.Persistence.Repositories.Abstract;

namespace Resources.Application.TruckManagement.Commands.CreateTruck
{
    public class CreateTruckCommandHandler : IRequestHandler<CreateTruckCommand, CreateTruckResponse>
    {
        private readonly ITruckRepository _truckRepository;
        public CreateTruckCommandHandler(ITruckRepository truckRepository)
        {
            Protector.Against.Null(truckRepository, nameof(truckRepository));

            _truckRepository = truckRepository;
        }

        public async Task<CreateTruckResponse> Handle(
            CreateTruckCommand request,
            CancellationToken cancellationToken)
        {
            var isExist = await _truckRepository.IsExistAsync(request.Code, cancellationToken);
            if (isExist)
                throw new ArgumentException($"Truck code '{request.Code}' exists in database");

            var truckToSave = MapTruckToSave(request);

            var truck = await _truckRepository.CreateTruckAsync(truckToSave, cancellationToken);
            return MapTruckToResponse(truck);
        }


        private static CreateTruckDto MapTruckToSave(CreateTruckCommand truckCommand)
        {
            return new CreateTruckDto
            {
                Code = truckCommand.Code,
                Name = truckCommand.Name,
                Status = truckCommand.Status,
                Description = truckCommand.Description
            };
        }

        private static CreateTruckResponse MapTruckToResponse(Truck truckCommand)
        {
            return new CreateTruckResponse
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
