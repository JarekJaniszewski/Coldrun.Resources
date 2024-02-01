using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Resources.Application.TruckManagement.Commands.CreateTruck;
using Resources.Application.UnitTests.Common;
using Resources.Domain.Entities;
using Resources.Domain.Models;
using Resources.Persistence.Repositories.Abstract;

namespace Resources.Application.UnitTests.Handlers
{
    public class CreateTruckCommandHandlerTests
    {
        [Test]
        [AutoMoqData]
        public async Task Handle_WhenTruckNotExists_ShouldBeAdded(
            [Frozen] Mock<ITruckRepository> truckRepoMock,
            CreateTruckCommand command,
            CreateTruckCommandHandler handler)
        {
            // Arrange
            truckRepoMock.Setup(x => x.IsExistAsync(command.Code, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            Truck truck = new()
            {
                Id = 1,
                Name = command.Name,
                Status = (byte)command.Status,
                Code = command.Code,
                Description = command.Description,
            };


            truckRepoMock.Setup(x => x.CreateTruckAsync(It.IsAny<CreateTruckDto>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(truck);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().NotBeNull();

            result.Id.Should().Be(truck.Id);
            result.Name.Should().Be(truck.Name);
            result.Status.Should().Be(command.Status);
            result.Description.Should().Be(command.Description);
            result.Code.Should().Be(command.Code);
        }

        [Test]
        [AutoMoqData]
        public async Task Handle_WhenTruckExists_ShouldThrow(
            [Frozen] Mock<ITruckRepository> truckRepoMock,
            CreateTruckCommand command,
            CreateTruckCommandHandler handler)
        {
            // Arrange
            truckRepoMock.Setup(x => x.IsExistAsync(command.Code, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            var act = async () => await handler.Handle(command, default);

            // Assert
            await act.Should().ThrowAsync<ArgumentException>().WithMessage($"Truck code '{command.Code}' exists in database");

        }
    }
}
