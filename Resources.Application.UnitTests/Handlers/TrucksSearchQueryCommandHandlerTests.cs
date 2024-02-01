using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;
using Resources.Application.UnitTests.Common;
using Resources.Application.TruckManagement.Queries.TrucksSearch;
using Resources.Domain.Entities;
using Resources.Domain.Enums;
using Resources.Persistence.Repositories.Abstract;
using Resources.Domain.Models;
using FluentAssertions;

namespace Resources.Application.UnitTests.Handlers
{
    public class TrucksSearchQueryCommandHandlerTests
    {
        [Test]
        [AutoMoqData]
        public async Task Handle_TrucksSearch_ReturnsCorrectly(
            [Frozen] Mock<ITruckRepository> truckRepoMock,
            TrucksSearchQueryCommand command,
            TrucksSearchQueryCommandHandler handler)
        {
            // Arrange
            var truckList = new List<Truck>
            {
                new()
                {
                    Id = 1,
                    Name = "Name",
                    Status = (byte)StatusType.Loading,
                    Code = "Code",
                    Description = "Description",
                },
                new()
                {
                    Id = 2,
                    Name = "Name2",
                    Status = (byte)StatusType.OutOfService,
                    Code = "Code2",
                    Description = "Description2",
                }
            };


            truckRepoMock.Setup(x => x.SearchTrucksAsync(It.IsAny<TruckSearchQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(truckList);

            truckRepoMock.Setup(x => x.GetTotalRecordCountsAsync(It.IsAny<TruckSearchQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(2);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().NotBeNull();
            result.PageCount = 1;
            result.RecordCount = 2;
            result.Items.Count().Should().Be(2);
        }
    }
}
