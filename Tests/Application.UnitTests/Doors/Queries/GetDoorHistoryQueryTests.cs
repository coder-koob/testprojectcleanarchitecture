using Application.Doors.Queries;
using Application.Doors.ReadModels;
using AutoFixture;
using Domain.Common.Exceptions;
using Domain.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace Application.UnitTests.Doors.Queries;

public class GetDoorHistoryQueryTests
{
    private readonly Fixture _fixture = new();

    [Fact]
    public async Task Should_Get_Door_History_Read_Model()
    {
        // Arrange
        var query = new GetDoorHistoryQuery(_fixture.Create<Guid>());

        var readModel = new Mock<DoorHistoryReadModel>().Object;

        var doorHistoryReadModelServiceMock = new Mock<IReadModelService<DoorHistoryReadModel>>();
        doorHistoryReadModelServiceMock
            .Setup(x =>
                x.GetByIdAsync(It.IsAny<string>()))
            .ReturnsAsync(readModel);

        var handler = new GetDoorHistoryQueryHandler(doorHistoryReadModelServiceMock.Object);

        // Act
        var response = await handler.Handle(query, new CancellationToken());

        // Assert
        response.Should().BeEquivalentTo(readModel, options => options.Excluding(o => o.Version));
    }

    [Fact]
    public async Task Should_Throw_NotFoundException_When_Read_Model_Cannot_Be_Found()
    {
        // Arrange
        var query = new GetDoorHistoryQuery(_fixture.Create<Guid>());

        var doorHistoryReadModelServiceMock = new Mock<IReadModelService<DoorHistoryReadModel>>();

        var handler = new GetDoorHistoryQueryHandler(doorHistoryReadModelServiceMock.Object);

        // Act
        var act = async () => await handler.Handle(query, new CancellationToken());

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}