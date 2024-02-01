using MediatR;
using Resources.Common.SafeGuards;
using Resources.Persistence.Repositories.Abstract;

namespace Resources.Application.TruckManagement.Commands.DeleteTruck
{
    public class DeleteTruckCommandHandler : IRequestHandler<DeleteTruckCommand, bool>
    {
        private readonly ITruckRepository _truckRepository;
        public DeleteTruckCommandHandler(ITruckRepository truckRepository)
        {
            Protector.Against.Null(truckRepository, nameof(truckRepository));

            _truckRepository = truckRepository;
        }

        public async Task<bool> Handle(
            DeleteTruckCommand request,
            CancellationToken cancellationToken)
        {
            var isExist = await _truckRepository.IsExistAsync(request.Code, cancellationToken);
            if (!isExist)
                throw new ArgumentException($"Truck code {request.Code} don't exist in database");

            await _truckRepository.DeleteTruckAsync(request.Code, cancellationToken);

            return true;
        }
    }
}
