using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Resources.Application.TruckManagement.Commands.UpdateTruck;
using Resources.Application.UnitTests.Common;
using Resources.Domain.Entities;
using Resources.Domain.Enums;
using Resources.Persistence.Repositories.Abstract;

namespace Resources.Application.UnitTests.Handlers
{
    public class UpdateTruckCommandHandlerTests
    {
        [Test]
        [AutoMoqData]
        public async Task Handle_WhenUpdateTruckWithCorrectStatus_ShouldBeUpdated1(
            [Frozen] Mock<ITruckRepository> truckRepoMock,
            UpdateTruckCommand command,
            UpdateTruckCommandHandler handler)
        {
            // Arrange
            truckRepoMock.Setup(x => x.IsExistAsync(command.Code, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);
            
            command.Status = StatusType.OutOfService;

            Truck truck = new()
            {
                Id = 1,
                Name = command.Name,
                Status = (byte)StatusType.Loading,
                Code = command.Code,
                Description = command.Description,
            };

            var truck2 = truck;
            truck2.Status = (byte)command.Status;

            truckRepoMock.Setup(x => x.GetTruckAsync(command.Code, It.IsAny<CancellationToken>()))
                .ReturnsAsync(truck);

            truckRepoMock.Setup(x => x.UpdateTruckAsync(It.IsAny<Truck>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(truck2);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().NotBeNull();

            result.Id.Should().Be(truck2.Id);
            result.Name.Should().Be(command.Name);
            result.Status.Should().Be(command.Status);
            result.Description.Should().Be(command.Description);
            result.Code.Should().Be(command.Code);
        }

        [Test]
        [AutoMoqData]
        public async Task Handle_WhenUpdateTruckWithCorrectStatus_ShouldBeUpdated2(
            [Frozen] Mock<ITruckRepository> truckRepoMock,
            UpdateTruckCommand command,
            UpdateTruckCommandHandler handler)
        {
            // Arrange
            truckRepoMock.Setup(x => x.IsExistAsync(command.Code, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            command.Status = StatusType.Loading;

            Truck truck = new()
            {
                Id = 1,
                Name = command.Name,
                Status = (byte)StatusType.Returning,
                Code = command.Code,
                Description = command.Description,
            };

            var truck2 = truck;
            truck2.Status = (byte)command.Status;

            truckRepoMock.Setup(x => x.GetTruckAsync(command.Code, It.IsAny<CancellationToken>()))
                .ReturnsAsync(truck);

            truckRepoMock.Setup(x => x.UpdateTruckAsync(It.IsAny<Truck>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(truck2);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().NotBeNull();

            result.Id.Should().Be(truck2.Id);
            result.Name.Should().Be(command.Name);
            result.Status.Should().Be(command.Status);
            result.Description.Should().Be(command.Description);
            result.Code.Should().Be(command.Code);
        }

        [Test]
        [AutoMoqData]
        public async Task Handle_WhenUpdateTruckWithCorrectStatus_ShouldBeUpdated3(
            [Frozen] Mock<ITruckRepository> truckRepoMock,
            UpdateTruckCommand command,
            UpdateTruckCommandHandler handler)
        {
            // Arrange
            truckRepoMock.Setup(x => x.IsExistAsync(command.Code, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            command.Status = StatusType.AtJob;

            Truck truck = new()
            {
                Id = 1,
                Name = command.Name,
                Status = (byte)StatusType.Loading,
                Code = command.Code,
                Description = command.Description,
            };

            var truck2 = truck;
            truck2.Status = (byte)command.Status;

            truckRepoMock.Setup(x => x.GetTruckAsync(command.Code, It.IsAny<CancellationToken>()))
                .ReturnsAsync(truck);

            truckRepoMock.Setup(x => x.UpdateTruckAsync(It.IsAny<Truck>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(truck2);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().NotBeNull();

            result.Id.Should().Be(truck2.Id);
            result.Name.Should().Be(command.Name);
            result.Status.Should().Be(command.Status);
            result.Description.Should().Be(command.Description);
            result.Code.Should().Be(command.Code);
        }

        [Test]
        [AutoMoqData]
        public async Task Handle_WhenUpdateTruckWithCorrectStatus_ShouldBeUpdated4(
            [Frozen] Mock<ITruckRepository> truckRepoMock,
            UpdateTruckCommand command,
            UpdateTruckCommandHandler handler)
        {
            // Arrange
            truckRepoMock.Setup(x => x.IsExistAsync(command.Code, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            command.Status = StatusType.AtJob;

            Truck truck = new()
            {
                Id = 1,
                Name = command.Name,
                Status = (byte)StatusType.OutOfService,
                Code = command.Code,
                Description = command.Description,
            };

            var truck2 = truck;
            truck2.Status = (byte)command.Status;

            truckRepoMock.Setup(x => x.GetTruckAsync(command.Code, It.IsAny<CancellationToken>()))
                .ReturnsAsync(truck);

            truckRepoMock.Setup(x => x.UpdateTruckAsync(It.IsAny<Truck>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(truck2);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().NotBeNull();

            result.Id.Should().Be(truck2.Id);
            result.Name.Should().Be(command.Name);
            result.Status.Should().Be(command.Status);
            result.Description.Should().Be(command.Description);
            result.Code.Should().Be(command.Code);
        }

        [Test]
        [AutoMoqData]
        public async Task Handle_WhenUpdateTruckWithNotCorrectStatus_ShouldThrow(
            [Frozen] Mock<ITruckRepository> truckRepoMock,
            UpdateTruckCommand command,
            UpdateTruckCommandHandler handler)
        {
            // Arrange
            truckRepoMock.Setup(x => x.IsExistAsync(command.Code, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

            command.Status = StatusType.Returning;

            Truck truck = new()
            {
                Id = 1,
                Name = command.Name,
                Status = (byte)StatusType.Loading,
                Code = command.Code,
                Description = command.Description,
            };


            truckRepoMock.Setup(x => x.GetTruckAsync(command.Code, It.IsAny<CancellationToken>()))
                .ReturnsAsync(truck);


            // Act
            var act = async () => await handler.Handle(command, default);

            // Assert
            await act.Should().ThrowAsync<ArgumentException>().WithMessage($"Update request with truck code {command.Code} doesn't have correct status");
        }

        [Test]
        [AutoMoqData]
        public async Task Handle_WhenNotTruckExists_ShouldThrow(
            [Frozen] Mock<ITruckRepository> truckRepoMock,
            UpdateTruckCommand command,
            UpdateTruckCommandHandler handler)
        {
            // Arrange
            truckRepoMock.Setup(x => x.IsExistAsync(command.Code, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act
            var act = async () => await handler.Handle(command, default);

            // Assert
            await act.Should().ThrowAsync<ArgumentException>().WithMessage($"Truck code {command.Code} don't exist in database");

        }
    }
}
