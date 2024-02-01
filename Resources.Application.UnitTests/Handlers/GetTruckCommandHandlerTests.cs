using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Resources.Application.TruckManagement.Queries.GetTruck;
using Resources.Application.UnitTests.Common;
using Resources.Domain.Entities;
using Resources.Domain.Enums;
using Resources.Persistence.Repositories.Abstract;

namespace Resources.Application.UnitTests.Handlers
{
    public class GetTruckCommandHandlerTests
    {
        [Test]
        [AutoMoqData]
        public async Task Handle_WhenTruckExists_ShouldBeRead(
            [Frozen] Mock<ITruckRepository> truckRepoMock,
            GetTruckCommand command,
            GetTruckCommandHandler handler)
        {
            // Arrange
            truckRepoMock.Setup(x => x.IsExistAsync(command.Code, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            Truck truck = new()
            {
                Id = 1,
                Name = "Name",
                Status = (byte)StatusType.Loading,
                Code = command.Code,
                Description = "Description",
            };

            truckRepoMock.Setup(x => x.GetTruckAsync(command.Code, It.IsAny<CancellationToken>()))
                .ReturnsAsync(truck);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().NotBeNull();

            result.Id.Should().Be(truck.Id);
            result.Name.Should().Be(truck.Name);
            result.Status.Should().Be((StatusType)truck.Status);
            result.Description.Should().Be(truck.Description);
            result.Code.Should().Be(command.Code);
        }

        [Test]
        [AutoMoqData]
        public async Task Handle_WhenTruckNotExists_ShouldThrow(
            [Frozen] Mock<ITruckRepository> truckRepoMock,
            GetTruckCommand command,
            GetTruckCommandHandler handler)
        {
            // Arrange
            command.Code = command.Code.Replace("-", "");

            truckRepoMock.Setup(x => x.IsExistAsync(command.Code, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act
            var act = async () => await handler.Handle(command, default);

            // Assert
            await act.Should().ThrowAsync<ArgumentException>().WithMessage($"Truck code {command.Code} don't exist in database");

        }
    }
}
