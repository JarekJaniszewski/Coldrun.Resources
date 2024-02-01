using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Resources.Application.TruckManagement.Commands.DeleteTruck;
using Resources.Application.UnitTests.Common;
using Resources.Persistence.Repositories.Abstract;

namespace Resources.Application.UnitTests.Handlers
{
    public class DeleteTruckCommandHandlerTests
    {
        [Test]
        [AutoMoqData]
        public async Task Handle_WhenTruckExists_ShouldBeDeleted(
            [Frozen] Mock<ITruckRepository> truckRepoMock,
            DeleteTruckCommand command,
            DeleteTruckCommandHandler handler)
        {
            // Arrange
            truckRepoMock.Setup(x => x.IsExistAsync(command.Code, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            truckRepoMock.Setup(x => x.DeleteTruckAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()));

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().BeTrue();
        }

        [Test]
        [AutoMoqData]
        public async Task Handle_WhenNotTruckExists_ShouldThrow(
            [Frozen] Mock<ITruckRepository> truckRepoMock,
            DeleteTruckCommand command,
            DeleteTruckCommandHandler handler)
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
