using MediatR;
using Resources.Common.SafeGuards;
using Resources.Domain.Entities;
using Resources.Domain.Enums;
using Resources.Persistence.Repositories.Abstract;

namespace Resources.Application.TruckManagement.Commands.UpdateTruck
{
    public class UpdateTruckCommandHandler : IRequestHandler<UpdateTruckCommand, UpdateTruckResponse>
    {
        private readonly ITruckRepository _truckRepository;
        public UpdateTruckCommandHandler(ITruckRepository truckRepository)
        {
            Protector.Against.Null(truckRepository, nameof(truckRepository));

            _truckRepository = truckRepository;
        }

        public async Task<UpdateTruckResponse> Handle(
            UpdateTruckCommand request,
            CancellationToken cancellationToken)
        {

            var isExist = await _truckRepository.IsExistAsync(request.Code, cancellationToken);
            if (!isExist)
                throw new ArgumentException($"Truck code {request.Code} don't exist in database");

            var truck = await _truckRepository.GetTruckAsync(request.Code, cancellationToken);
            var condition = StatusVerification(request.Status, truck.Status);
            if (!condition)
            {
                throw new ArgumentException($"Update request with truck code {request.Code} doesn't have correct status");
            }

            truck.Name = request.Name;
            truck.Description = request.Description;
            truck.Status = (byte)request.Status;

            return MapTruckToResponse(await _truckRepository.UpdateTruckAsync(truck, cancellationToken));
        }


        private static bool StatusVerification(StatusType reqStatus, int dbStatus)
        {
            if (reqStatus == StatusType.OutOfService)
            {
                return true;
            }
            else
            {
                switch ((StatusType)dbStatus)
                {
                    case StatusType.OutOfService:
                        {
                            return true;
                        }

                    case StatusType.Returning:
                        {
                            if (reqStatus == StatusType.Loading)
                                return true;
                            else
                                return false;
                        }
                    default:
                        {
                            var result =  dbStatus - (int)reqStatus;
                            if (result is -1 or 0)
                                return true;
                            else
                                return false;

                        }
                }
            }

        }

        private static UpdateTruckResponse MapTruckToResponse(Truck truckCommand)
        {
            return new UpdateTruckResponse
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
