using Application.Doors.Commands;
using AutoFixture;
using Domain.Common.Exceptions;
using Domain.Doors;
using Domain.Doors.Commands;
using Domain.Interfaces;
using Domain.Offices;
using FluentAssertions;
using FluentAssertions.Execution;
using Moq;
using Xunit;

namespace Application.UnitTests.Doors.Commands;

public class AddDoorCommandTests
{
    private readonly Fixture _autoFixture = new();

    [Fact]
    public async Task Should_Return_Create_And_Return_Door()
    {
        // Arrange
        var officeId = _autoFixture.Create<Guid>();
        var officeName = _autoFixture.Create<string>();

        var doorId = _autoFixture.Create<Guid>();
        var doorName = _autoFixture.Create<string>();
        var scope = _autoFixture.Create<string>();

        var office = new Office(officeId, officeName);
        var door = new Door(officeId, doorId, doorName, scope);

        var command = new AddDoorCommand(new AddDoorPayload(officeId, doorId, doorName, scope));

        var eventSourceRepoMock = new Mock<IEventSourcedRepository<Office>>();
        eventSourceRepoMock.Setup(x => x.GetByIdAsync(officeId)).ReturnsAsync(office);

        var handler = new AddDoorCommandHandler(eventSourceRepoMock.Object);

        // Act
        var result = await handler.Handle(command, new CancellationToken());

        // Assert
        using (new AssertionScope())
        {
            eventSourceRepoMock.Verify(x => x.SaveAsync(office), Times.Once);

            result.Should().NotBeNull();
            result.Should().BeOfType<Door>();
            result.Should().BeEquivalentTo(door);
        }
    }

    [Fact]
    public async Task Should_Throw_NotFoundException_When_Office_Cannot_Be_Found()
    {
        // Arrange
        var officeId = _autoFixture.Create<Guid>();
        var officeName = _autoFixture.Create<string>();

        var doorId = _autoFixture.Create<Guid>();
        var doorName = _autoFixture.Create<string>();
        var scope = _autoFixture.Create<string>();

        var command = new AddDoorCommand(new AddDoorPayload(officeId, doorId, doorName, scope));

        var eventSourceRepoMock = new Mock<IEventSourcedRepository<Office>>();

        var handler = new AddDoorCommandHandler(eventSourceRepoMock.Object);

        // Act
        var act = async () => await handler.Handle(command, new CancellationToken());

        // Assert
        using (new AssertionScope())
        {
            await act.Should().ThrowAsync<NotFoundException>();

            eventSourceRepoMock.Verify(x => x.SaveAsync(It.IsAny<Office>()), Times.Never);
        }
    }
}